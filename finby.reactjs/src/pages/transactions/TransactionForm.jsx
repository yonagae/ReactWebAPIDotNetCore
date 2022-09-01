import { useState, useEffect } from 'react';
import api from '../../api/transaction';
import TransactionAmountsForm from '../transactionAmounts/TransactionAmountsForm';
import DatePicker from "react-datepicker";
import Button from 'react-bootstrap/Button';
import ToggleButton from 'react-bootstrap/ToggleButton';
import ButtonGroup from 'react-bootstrap/ButtonGroup';

const initialTransaction = {
    id: 0,
    date: new Date(),
    transactionTypeId: 0,
    transactionType: {},
    userId: 1,
    description: '',
    shortDescription: '',
    totalAmount: 0,
    transactionAmounts: [{ id: 0, userId: '0', amount: '0' }],
    flow: 'c'.charCodeAt(0)
};

export default function TransactionForm(props) {
    const [transaction, setTransaction] = useState(transactionAtual());
    const [transactionTypes, setTransactionTypes] = useState([]);
    const [startDate, setStartDate] = useState(new Date());

    useEffect(() => {
        if (props.ativSelecionada.id !== 0) {
            setTransaction(props.ativSelecionada);
            setStartDate(new Date(transaction.date));
        }
    }, [props.ativSelecionada]);

    useEffect(() => {
        getTransactionTypes().then(result => setTransactionTypes(result));
    }, []);    

    const inputTextHandler = (e) => {
        const { name, value } = e.target;

        setTransaction({ ...transaction, [name]: value });
    };

    const inputSelectHandler = (e) => {
        const { name, value } = e.target;
        var tp = transactionTypes.filter(obj => {
            return obj.id == value
        })
        setTransaction({ ...transaction, [name]: value, transactionType: tp[0]});
    };

    const updateTotalAmount = () => {
        const totalAmountSum = transaction.transactionAmounts.reduce(
            (accumulator, transAmount) => accumulator + parseFloat(transAmount.amount), 0);

        if (isNaN(totalAmountSum)) 
            setTransaction({ ...transaction, totalAmount: 0 });
        else
            setTransaction({ ...transaction, totalAmount: totalAmountSum });
    };

    const handleSubmit = (e) => {
        e.preventDefault();

        transaction.date = startDate;

        if (props.ativSelecionada.id !== 0) props.atualizarTransaction(transaction);
        else props.addTransaction(transaction);

        setTransaction(initialTransaction);
    };

    const handleCancelar = (e) => {
        e.preventDefault();

        props.cancelarTransaction();

        setTransaction(initialTransaction);
    };

    function transactionAtual() {
        if (props.ativSelecionada.id !== 0) {
            return props.ativSelecionada;
        } else {
            return initialTransaction;
        }
    }

    async function getTransactionTypes() {
        try {
            const response = await api.get('transactionTypes');
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

    const expenseDepositRadiosOptions = [
        { name: 'Expense', value: 'c'.charCodeAt(0) },
        { name: 'Deposit', value: 'd'.charCodeAt(0)},
    ];

    const changeExpenseOrDepositFlag = (e) => {
        const { name, value } = e.target;

        setTransaction({ ...transaction, [name]: parseInt(value) });
    }

    return (
        <>
            <form className='row g-3' onSubmit={handleSubmit}>
                <ButtonGroup>
                    {expenseDepositRadiosOptions.map((radio, idx) => (
                        <ToggleButton
                            key={idx}
                            id={`radio-${idx}`}
                            type="radio"
                            variant={idx % 2 ? 'outline-success' : 'outline-danger'}
                            name="flow"
                            value={radio.value}
                            checked={transaction.flow == radio.value}
                            onChange={changeExpenseOrDepositFlag}
                        >
                            {radio.name}
                        </ToggleButton>
                    ))}
                </ButtonGroup>
                <br />

                <div className='col-md-6'>
                    <label className='form-label'>Short Descrition</label>
                    <input
                        name='shortDescription'
                        value={transaction.shortDescription}
                        onChange={inputTextHandler}
                        id='shortDescription'
                        type='text'
                        className='form-control'
                        required
                    />
                </div>
                <div className='col-md-6'>
                    <label className='form-label'>TransactionType</label>
                    <select
                        name='transactionTypeId'
                        value={transaction.transactionTypeId}
                        onChange={inputSelectHandler}
                        id='transactionTypeId'
                        className='form-select'
                    >
                        <option value='NaoDefinido'>Select...</option>
                        {transactionTypes.map((transtype) => (
                            <option key={transtype.id} value={transtype.id}>{transtype.name}</option>
                        ))}                           
                       
                    </select>
                </div>                
                <div className='col-md-6'>
                    <label className='form-label'>Date</label>
                    <style>
                        {`.date-picker input {
                              width: 100%;
                              height: 38px;
                          }`}
                    </style>
                    <DatePicker selected={startDate}
                        onChange={(date: Date) => setStartDate(date)}
                        dateFormat="dd/MM/yyyy"
                        wrapperClassName="date-picker"
                        className='form-control'
                        id='datePicker'
                    />
                </div>
                <div className='col-md-6'>
                    <label className='form-label'>Total Amount</label>
                    <input
                        name='totalAmount'
                        disabled
                        value={transaction.totalAmount}
                        id='totalAmount'
                        type='text'
                        className='form-control readonly'
                        aria-label='Disabled input example'
                    />
                </div>
                <div className='col-md-12'>
                    <label className='form-label'>Description</label>
                    <textarea
                        name='description'
                        value={transaction.description}
                        onChange={inputTextHandler}
                        id='description'
                        type='text'
                        className='form-control'
                    />
                    <hr />
                </div>               

                <TransactionAmountsForm
                    transaction={transaction}
                    updateTotalAmount={updateTotalAmount}
                />

                <div className='col-12 mt-0'>
                    {transaction.id === 0 ? (
                        <button
                            className='btn btn-outline-success'
                            type='submit'
                            id="saveNewTransactionBtn"
                        >
                            <i className='fas fa-plus me-2'></i>
                            Salvar
                        </button>
                    ) : (
                        <>
                            <button
                                className='btn btn-info me-2'
                                type='submit'
                            >
                                <i className='fas fa-save me-2'></i>
                                Save
                            </button>
                            <button
                                className='btn btn-warning'
                                onClick={handleCancelar}
                            >
                                <i className='fas fa-ban me-2'></i>
                                Cancel
                            </button>
                        </>
                    )}
                </div>
            </form>
        </>
    );
}
