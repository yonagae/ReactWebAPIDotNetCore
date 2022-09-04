import React from 'react';
import TransacationTypeItem from './TransacationTypeItem';

export default function TransacationTypeLista(props) {
    return (
        <div className='mt-3'>
            <table id="transactionsTable" className='table table-striped table-hover'>
                <thead className='table-dark mt-3'>
                    <tr>
                        <th scope='col'>Name</th>
                        <th scope='col'>Color</th>
                        <th scope='col'>Options</th>
                    </tr>
                </thead>
                <tbody>

                    {props.transactionTypes.map((ativ) => (
                        <TransacationTypeItem
                            key={ativ.id}
                            ativ={ativ}
                            pegarAtividade={props.pegarAtividade}
                            handleConfirmModal={props.handleConfirmModal}
                        />
                    ))}

                </tbody>
            </table>
        </div>
    );
}
