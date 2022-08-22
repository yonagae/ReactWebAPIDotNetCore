import React from 'react';
import TransactionItem from './TransactionItem';

export default function TransactionLista(props) {
    return (
        <div className='mt-3'>
            {props.transactions.map((ativ) => (
                <TransactionItem
                    key={ativ.id}
                    ativ={ativ}
                    pegarTransaction={props.pegarTransaction}
                    handleConfirmModal={props.handleConfirmModal}
                />
            ))}
        </div>
    );
}
