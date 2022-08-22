import React from 'react';

export default function TransactionItem(props) {
    function prioridadeLabel(param) {
        return 'Não definido';
    }

    function prioridadeStyle(param, icone) {

        return icone ? 'smile' : 'success';
    }

    return (
        <div
            className={
                'card mb-2 shadow-sm border-success'
            }
        >
            <div className='card-body'>
                <div className='d-flex justify-content-between'>
                    <h5 className='card-title'>
                        <span className='badge bg-secondary me-1'>
                            {props.ativ.id}
                        </span>
                        - {props.ativ.shortDescription}
                    </h5>
                    <h6>
                        Prioridade:
                        <span
                            className={
                                'ms-1 text-success'
                            }
                        >
                            <i
                                className={
                                    'me-1 far fa-smile'
                                }
                            ></i>
                            {prioridadeLabel(props.ativ.transactionTypeId)}
                        </span>
                    </h6>
                </div>
                <p className='card-text'>{props.ativ.description}</p>
                <div className='d-flex justify-content-end pt-2 m-0 border-top'>
                    <button
                        className='btn btn-sm btn-outline-primary me-2'
                        onClick={() => props.pegarTransaction(props.ativ.id)}
                    >
                        <i className='fas fa-pen me-2'></i>
                        Editar
                    </button>
                    <button
                        className='btn btn-sm btn-outline-danger'
                        onClick={() => props.handleConfirmModal(props.ativ.id)}
                    >
                        <i className='fas fa-trash me-2'></i>
                        Deletar
                    </button>
                </div>
            </div>
        </div>
    );
}
