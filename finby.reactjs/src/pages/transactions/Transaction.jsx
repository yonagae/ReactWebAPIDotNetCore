import { useState, useEffect } from 'react';
import { Button, Modal } from 'react-bootstrap';
import TransactionForm from './TransactionForm';
import TransactionLista from './TransactionLista';
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
        const response = await api.get('transactions');
        return response.data;
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
        const response = await api.post('transaction', ativ);
        console.log(response.data);
        setTransactions([...transactions, response.data]);
    };

    const cancelarTransaction = () => {
        setTransaction({ id: 0 });
        handleAtiviadeModal();
    };

    const atualizarTransaction = async (ativ) => {
        handleAtiviadeModal();
        const response = await api.put(`transaction/${ativ.id}`, ativ);
        const { id } = response.data;
        setTransactions(
            transactions.map((item) => (item.id === id ? response.data : item))
        );
        setTransaction({ id: 0 });
    };

    const deletarTransaction = async (id) => {
        handleConfirmModal(0);
        if (await api.delete(`transaction/${id}`)) {
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

    return (
        <>
            <TitlePage
                title={'Transaction ' + (transaction.id !== 0 ? transaction.id : '')}
            >
                <Button variant='outline-secondary' onClick={novaTransaction}>
                    <i className='fas fa-plus'></i>
                </Button>
            </TitlePage>

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