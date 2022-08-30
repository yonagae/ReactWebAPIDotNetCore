import { useState, useEffect } from 'react';
import { Button, Modal } from 'react-bootstrap';
import TransactionForm from './TransactionForm';
import TransactionLista from './TransactionLista';
import CustomLabelPieChart from '../../components/CustomLabelPieChart'
import api from '../../api/transaction';
import TitlePage from '../../components/TitlePage';

export default function Transaction() {
    const [showTransactionModal, setShowTransactionModal] = useState(false);
    const [smShowConfirmModal, setSmShowConfirmModal] = useState(false);

    const [transactions, setTransactions] = useState([]);
    const [transaction, setTransaction] = useState({ id: 0 });

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
            const response = await api.get('transactions');
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
    }, []);

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
                if (object.transactionType.name == element)
                    return accumulator + object.totalAmount;
                else
                    return accumulator;
            }, 0);
            pieChartData.push({ name: element, value: sum });
        });


        return  pieChartData ;
    }

    return (
        <>
            <TitlePage
                title={'Transaction ' + (transaction.id !== 0 ? transaction.id : '')}
            >
                <Button id="newTransactionButton" variant='outline-secondary' onClick={novaTransaction}>
                    <i className='fas fa-plus'></i>
                </Button>
            </TitlePage>

            <CustomLabelPieChart
                fillPieData={fillPieData}
            />

            <TransactionLista
                transactions={transactions}
                pegarTransaction={pegarTransaction}
                handleConfirmModal={handleConfirmModal}
            />

            <Modal show={showTransactionModal} onHide={handleAtiviadeModal}>
                <Modal.Header closeButton>
                    <Modal.Title>
                        Transaction {transaction.id !== 0 ? transaction.id : ''}
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
