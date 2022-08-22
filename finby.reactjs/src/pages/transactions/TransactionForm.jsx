import { useState, useEffect } from 'react';

const transactionInicial = {
    id: 0,
    date: new Date(),
    transactionTypeId: 0,
    userId: 0,
    description: 'Description',
    shortDescription: 'Shor Description',
    totalAmount: 0,
};

export default function TransactionForm(props) {
    const [transaction, setTransaction] = useState(transactionAtual());

    useEffect(() => {
        if (props.ativSelecionada.id !== 0) setTransaction(props.ativSelecionada);
    }, [props.ativSelecionada]);

    const inputTextHandler = (e) => {
        const { name, value } = e.target;

        setTransaction({ ...transaction, [name]: value });
    };

    const handleSubmit = (e) => {
        e.preventDefault();

        if (props.ativSelecionada.id !== 0) props.atualizarTransaction(transaction);
        else props.addTransaction(transaction);

        setTransaction(transactionInicial);
    };

    const handleCancelar = (e) => {
        e.preventDefault();

        props.cancelarTransaction();

        setTransaction(transactionInicial);
    };

    function transactionAtual() {
        if (props.ativSelecionada.id !== 0) {
            return props.ativSelecionada;
        } else {
            return transactionInicial;
        }
    }

    return (
        <>
            <form className='row g-3' onSubmit={handleSubmit}>
                <div className='col-md-6'>
                    <label className='form-label'>Short Descrition</label>
                    <input
                        name='shortDescription'
                        value={transaction.shortDescription}
                        onChange={inputTextHandler}
                        id='shortDescription'
                        type='text'
                        className='form-control'
                    />
                </div>
                <div className='col-md-6'>
                    <label className='form-label'>TransactionType</label>
                    <select
                        name='transactionTypeId'
                        value={transaction.transactionTypeId}
                        onChange={inputTextHandler}
                        id='transactionTypeId'
                        className='form-select'
                    >
                        <option value='0'>Selecione...</option>
                        <option value='1'>Baixa</option>
                        <option value='2'>Normal</option>
                        <option value='3'>Alta</option>
                        <option value='4'>Alta</option>
                        <option value='5'>Alta</option>
                        <option value='6'>Alta</option>
                        <option value='7'>Alta</option>
                        <option value='8'>Alta</option>
                        <option value='9'>Alta</option>
                    </select>
                </div>

                <div className='col-md-6'>
                    <label className='form-label'>Total Amount</label>
                    <input
                        name='totalAmount'
                        value={transaction.totalAmount}
                        onChange={inputTextHandler}
                        id='totalAmount'
                        type='text'
                        className='form-control'
                    />
                </div>
                <div className='col-md-6'>
                    <label className='form-label'>Date</label>
                    <input
                        name='date'
                        value={transaction.date}
                        onChange={inputTextHandler}
                        id='date'
                        type='text'
                        className='form-control'
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
                <div className='col-12 mt-0'>
                    {transaction.id === 0 ? (
                        <button
                            className='btn btn-outline-success'
                            type='submit'
                        >
                            <i className='fas fa-plus me-2'></i>
                            Salvar
                        </button>
                    ) : (
                        <>
                            <button
                                className='btn btn-outline-success me-2'
                                type='submit'
                            >
                                <i className='fas fa-plus me-2'></i>
                                Salvar
                            </button>
                            <button
                                className='btn btn-outline-warning'
                                onClick={handleCancelar}
                            >
                                <i className='fas fa-plus me-2'></i>
                                Cancelar
                            </button>
                        </>
                    )}
                </div>
            </form>
        </>
    );
}
