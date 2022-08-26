import TransactionItem from './TransactionItem';

export default function TransactionLista(props) {
    return (
        <div className='mt-3'>

            <table className='table table-striped table-hover'>
                <thead className='table-dark mt-3'>
                    <tr>
                        <th scope='col'>Description</th>
                        <th scope='col'>Date</th>
                        <th scope='col'>Type</th>
                        <th scope='col'>User</th>
                        <th scope='col'>Amount</th>
                        <th scope='col'>Options</th>
                    </tr>
                </thead>
                <tbody>

                    {props.transactions.map((transaction) => (
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