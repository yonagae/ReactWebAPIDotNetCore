import TransactionItem from './TransactionItem';
import { useState } from 'react';
import { Button, FormControl, InputGroup } from 'react-bootstrap';

export default function TransactionLista(props) {
    const [termoBusca, setTermoBusca] = useState('');

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
                        <th scope='col'>Description</th>
                        <th scope='col'>Date</th>
                        <th scope='col'>Type</th>
                        <th scope='col'>User</th>
                        <th scope='col'>Flow</th>
                        <th scope='col'>Amount</th>
                        <th scope='col'>Options</th>
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