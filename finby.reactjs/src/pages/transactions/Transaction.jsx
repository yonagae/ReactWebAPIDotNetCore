import { useState, useEffect } from 'react';
import { Button, Modal } from 'react-bootstrap';
import TransactionForm from './TransactionForm';
import TransactionLista from './TransactionLista';
import CustomLabelPieChart from '../../components/CustomLabelPieChart'
import api from '../../api/transaction';
import TitlePage from '../../components/TitlePage';
import DatePicker from "react-datepicker";

export default function Transaction() {
    const [showTransactionModal, setShowTransactionModal] = useState(false);
    const [smShowConfirmModal, setSmShowConfirmModal] = useState(false);

    const [transactions, setTransactions] = useState([]);
    const [transaction, setTransaction] = useState({ id: 0 });

    const [startDate, setStartDate] = useState(new Date(2022, 0, 1));
    const [endDate, setEndDate] = useState(new Date(2022, 1, 1));

    const handleAtiviadeModal = () =>
        setShowTransactionModal(!showTransactionModal);

    const handleConfirmModal = (id) => {
        if (id !== 0 && id !== undefined) {
            const transaction = transactions.filter(
                (transaction) => transaction.id === id
            );
            setTransaction(transaction[0]);
        } else {
            setTransaction({ id: 0 });
        }
        setSmShowConfirmModal(!smShowConfirmModal);
    };

    const pegaTodasTransactions = async () => {
        try {
            const response = await api.get('transactions', { params: { start: startDate.toLocaleDateString('en-CA'), end: endDate.toLocaleDateString('en-CA') } });
            return response.data;
        } catch (err) {
            if (err.response) {
               console.log('APIError', err.message);
            } else if (err.request) {
               console.log('RequestError', err.message);
            } else {
                console.log('Error', err.message);
            }
        }
    };

    const novaTransaction = () => {
        setTransaction({ id: 0 });
        handleAtiviadeModal();
    };

    useEffect(() => {
        const getTransactions = async () => {
            const todasTransactions = await pegaTodasTransactions();
            if (todasTransactions) setTransactions(todasTransactions);
        };
        getTransactions();
    }, endDate);

    const addTransaction = async (ativ) => {
        handleAtiviadeModal();
        const response = await api.post('transactions', ativ);
        console.log(response.data);
        setTransactions([...transactions, response.data]);
    };

    const cancelarTransaction = () => {
        setTransaction({ id: 0 });
        handleAtiviadeModal();
    };

    const atualizarTransaction = async (ativ) => {
        handleAtiviadeModal();
        const response = await api.put(`transactions/${ativ.id}`, ativ);
        const { id } = response.data;
        setTransactions(
            transactions.map((item) => (item.id === id ? response.data : item))
        );
        setTransaction({ id: 0 });
    };

    const deletarTransaction = async (id) => {
        handleConfirmModal(0);
        if (await api.delete(`transactions/${id}`)) {
            const transactionsFiltradas = transactions.filter(
                (transaction) => transaction.id !== id
            );
            setTransactions([...transactionsFiltradas]);
        }
    };

    const pegarTransaction = (id) => {
        const transaction = transactions.filter((transaction) => transaction.id === id);
        setTransaction(transaction[0]);
        handleAtiviadeModal();
    };

    const fillPieData = () => {
        let pieChartData = [];
        const transactionTypeNames = transactions.map(item => item.transactionType.name)
            .filter((value, index, self) => self.indexOf(value) === index);

        transactionTypeNames.forEach(element => {
            const sum = transactions.reduce((accumulator, object) => {
                if (object.transactionType.name == element
                    && String.fromCharCode(object.flow) == 'c')
                    return accumulator + object.totalAmount;
                else
                    return accumulator;
            }, 0);

            const argbColor = transactions.find(tr => {
                return tr.transactionType.name == element;
            }).transactionType.argbColor;

            pieChartData.push({ name: element, value: sum, argbColor: argbColor});
        });


        return  pieChartData ;
    }


    const onDatePickerChange = (dates) => {
        const [start, end] = dates;
        setStartDate(start);
        setEndDate(end);
    };

    return (
        <>
            <TitlePage
                title={'Transactions '}
            >
                <Button id="newTransactionButton" variant='outline-secondary' onClick={novaTransaction}>
                    <i className='fas fa-plus'></i>
                </Button>
            </TitlePage>

            <CustomLabelPieChart
                fillPieData={fillPieData}
            />

            <div className='col-sm-12'>
                <label className='form-label'>Pick a data range:</label>
            </div>
            <div className='col-sm-12'>
                <DatePicker
                    onChange={onDatePickerChange}
                    startDate={startDate}
                    endDate={endDate}
                    dateFormat="dd/MM/yyyy"
                    wrapperClassName="date-picker"
                    className='form-control col-sm-6'
                    id='datePicker'
                    selectsRange
                />
            </div>

            <TransactionLista
                transactions={transactions}
                pegarTransaction={pegarTransaction}
                handleConfirmModal={handleConfirmModal}
                setTransactions={setTransactions}
            />

            <Modal show={showTransactionModal} onHide={handleAtiviadeModal}>
                <Modal.Header closeButton>
                    <Modal.Title>
                        {transaction.shortDescription}
                    </Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <TransactionForm
                        addTransaction={addTransaction}
                        cancelarTransaction={cancelarTransaction}
                        atualizarTransaction={atualizarTransaction}
                        ativSelecionada={transaction}
                        transactions={transactions}
                    />
                </Modal.Body>
            </Modal>

            <Modal
                size='sm'
                show={smShowConfirmModal}
                onHide={handleConfirmModal}
            >
                <Modal.Header closeButton>
                    <Modal.Title>
                        Excluindo Transaction{' '}
                        {transaction.id !== 0 ? transaction.id : ''}
                    </Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    Tem certeza que deseja Excluir a Transaction {transaction.id}
                </Modal.Body>
                <Modal.Footer className='d-flex justify-content-between'>
                    <button
                        className='btn btn-outline-success me-2'
                        onClick={() => deletarTransaction(transaction.id)}
                    >
                        <i className='fas fa-check me-2'></i>
                        Sim
                    </button>
                    <button
                        className='btn btn-danger me-2'
                        onClick={() => handleConfirmModal(0)}
                    >
                        <i className='fas fa-times me-2'></i>
                        Não
                    </button>
                </Modal.Footer>
            </Modal>
        </>
    );
}
