import React from 'react';

export default function TransactionItem(props) {
    function prioridadeLabel(param) {
        return 'Não definido';
    }

    function prioridadeStyle(param, icone) {

        return icone ? 'smile' : 'success';
    }

    return (

        <tr key={props.ativ.id}>
            <td>{props.ativ.shortDescription}</td>
            <td>{new Date(props.ativ.date).toLocaleDateString()}</td>
            <td>{props.ativ.transactionType.name}</td>
            <td>{props.ativ.user.name}</td>
            <td>
                <div text-align='right'>
                    {"$" + props.ativ.totalAmount.toFixed(2)}
                </div>
            </td>
            <td>
                <div>
                    <button
                        className='btn btn-sm btn-success me-2'
                        onClick={() => props.pegarTransaction(props.ativ.id)}
                    >
                        <i className='fas fa-pen me-2'></i>
                        Edit
                    </button>
                    <button
                        className='btn btn-sm btn-primary'
                        onClick={() => props.handleConfirmModal(props.ativ.id)}
                    >
                        <i className='fas fa-trash me-2'></i>
                        Delete
                    </button>
                </div>
            </td>
        </tr>      
    );
}
