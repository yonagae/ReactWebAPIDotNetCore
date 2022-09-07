import React from 'react';
import TransacationTypeItem from './TransacationTypeItem';

export default function TransacationTypeLista(props) {

    const cancelarAtividade = (elementID) => {
        var elementClassList = document.getElementById(elementID).classList;

        if (elementClassList.contains('fa-caret-down')) {
            elementClassList.remove('fa-caret-down');
            elementClassList.add('fa-caret-up');
        } else {
            elementClassList.remove('fa-caret-up');
            elementClassList.add('fa-caret-down');
        }
    };

    return (
        <div className='mt-3'>
            <table id="transactionsTable" className='table table-striped table-hover'>
                <thead className='table-dark mt-3'>
                    <tr>
                        <th scope='col'>
                            Name &nbsp;
                            <i id='nameHeader' className='fa-sharp fa-solid fa-caret-down' onClick={() => { cancelarAtividade('nameHeader') } }></i>
                        </th>
                        <th scope='col'>
                            Color
                        </th>
                        <th scope='col'>
                            Options 
                        </th>
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
