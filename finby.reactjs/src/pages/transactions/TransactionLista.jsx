import TransactionItem from './TransactionItem';
import { useState } from 'react';
import { Button, FormControl, InputGroup } from 'react-bootstrap';

export default function TransactionLista(props) {
    const [termoBusca, setTermoBusca] = useState('');
    const [, updateState] = useState(); //this is used to force update when the sort is activated


    const handleInputChange = (e) => {
        setTermoBusca(e.target.value);
    };

    const filteredTransactions = props.transactions.filter((transaction) => {

        var searchableText = transaction.description;
        searchableText += transaction.shortDescription + ' ';
        searchableText += transaction.user.name + ' ';
        searchableText += transaction.transactionType.name + ' ';
        searchableText += transaction.totalAmount + ' ';

        return searchableText.toLowerCase()
            .includes(termoBusca.toLowerCase());
    });

    function compareDate(property) {
        return function (a, b) {
            return property ? new Date(a.date) - new Date(b.date) : new Date(b.date) - new Date(a.date);
        }
    }

    function compareString(a, b) {
        if (a < b) return -1;
        if (a > b) return 1;
        return 0;
    }

    function compareType(property) {
        return function (a, b) {
            return property ? compareString(a.transactionType.name, b.transactionType.name) : compareString(b.transactionType.name, a.transactionType.name);
        }
    }

    function compareUser(property) {
        return function (a, b) {
            return property ? compareString(a.user.name, b.user.name) : compareString(b.user.name, a.user.name);
        }
    }

    function compareAmount(property) {
        return function (a, b) {
            return property ? a.totalAmount - b.totalAmount : b.totalAmount - a.totalAmount;
        }
    }
    
    const sortTable = (elementID, compare) => {
        var elementClassList = document.getElementById(elementID).classList;

        if (elementClassList.contains('fa-caret-down')) {
            elementClassList.remove('fa-caret-down');
            elementClassList.add('fa-caret-up');
            props.transactions.sort(compare(false));
        } else {
            elementClassList.remove('fa-caret-up');
            elementClassList.add('fa-caret-down');
            props.transactions.sort(compare(true));
        }
        props.setTransactions(props.transactions);
        updateState({});
    };


    return (
        <div className='mt-3'>
            
            <InputGroup className='mt-3 mb-3'>
                <InputGroup.Text>Search:</InputGroup.Text>
                <FormControl
                    onChange={handleInputChange}
                    placeholder='Search'
                />
            </InputGroup>
            <table id="transactionsTable" className='table table-striped table-hover'>
                <thead className='table-dark mt-3'>
                    <tr>
                        <th scope='col'>Description
                        </th>
                        <th scope='col'>Date &nbsp;
                            <i id='dateHeader' className='fa-sharp fa-solid fa-caret-down' onClick={() => { sortTable('dateHeader', compareDate)}}>
                            </i>
                        </th>
                        <th scope='col'>Type &nbsp;
                            <i id='typeHeader' className='fa-sharp fa-solid fa-caret-down' onClick={() => { sortTable('typeHeader', compareType)}}></i>
                        </th>
                        <th scope='col'>User &nbsp;
                            <i id='userHeader' className='fa-sharp fa-solid fa-caret-down' onClick={() => { sortTable('userHeader', compareUser) }}></i>
                        </th>
                        <th scope='col'>Amount &nbsp;
                            <i id='amountHeader' className='fa-sharp fa-solid fa-caret-down' onClick={() => { sortTable('amountHeader', compareAmount) }}></i>
                        </th>
                        <th scope='col'>Options
                        </th>
                    </tr>
                </thead>
                <tbody>

                    {filteredTransactions.map((transaction) => (
                        <TransactionItem
                            key={transaction.id}
                            transaction={transaction}
                            pegarTransaction={props.pegarTransaction}
                            handleConfirmModal={props.handleConfirmModal}
                        />
                    ))}

                </tbody>
            </table>
        </div>
    );
}