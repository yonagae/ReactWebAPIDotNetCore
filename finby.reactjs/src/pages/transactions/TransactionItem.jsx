import React from 'react';

export default function TransactionItem(props) {
    return (

        <tr key={props.transaction.id}>
            <td>{props.transaction.shortDescription}</td>
            <td>{new Date(props.transaction.date).toLocaleDateString()}</td>
            <td>{props.transaction.transactionType.name}</td>
            <td>{props.transaction.user.name}</td>
            <td>
                <div text-align='right'>
                    {"$" + props.transaction.totalAmount.toFixed(2)}
                </div>
            </td>
            <td>
                <div>
                    <button
                        className='btn btn-sm btn-success me-2'
                        onClick={() => props.pegarTransaction(props.transaction.id)}
                    >
                        <i className='fas fa-pen me-2'></i>
                        Edit
                    </button>
                    <button
                        className='btn btn-sm btn-primary'
                        onClick={() => props.handleConfirmModal(props.transaction.id)}
                    >
                        <i className='fas fa-trash me-2'></i>
                        Delete
                    </button>
                </div>
            </td>
        </tr>      
    );
}
