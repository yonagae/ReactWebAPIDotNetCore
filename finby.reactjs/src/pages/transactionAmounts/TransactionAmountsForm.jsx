import { useState, useEffect } from 'react';
import { Alert } from 'react-bootstrap';
import api from '../../api/transaction';
import './TransactionAmountsForm.css';

export default function TransactionAmountsForm(props) {
    const [formFields, setFormFields] = useState(null)
    const [users, setUsers] = useState([]);

    async function getAllTransactionAmounts() {
        if (props.transaction.id <= 0) setFormFields([{ id: '0', userId: '0', positiveamount: '' }]);

        try {
            const response = await api.get(`transactions/${props.transaction.id}/TransactionAmounts`);
            props.transaction.transactionAmounts = response.data;
            setFormFields(response.data);
        } catch (err) {
            if (err.response) {
               console.log('APIError', err.message);
            } else if (err.request) {
               console.log('RequestError', err.message);
            } else {
                console.log('Error', err.message);
            }
        }
    }

    async function getAllUsers() {
        try {
            const response = await api.get('users');
            return response.data;
        } catch (err) {
            if (err.response) {
               console.log('APIError', err.message);
            } else if (err.request) {
               console.log('RequestError', err.message);
            } else {
                console.log('Error', err.message);
            }
        }
    };

    useEffect(() => {
        getAllUsers().then(result => setUsers(result));
        getAllTransactionAmounts();
    }, []);

    const preventPasteNegative = (e) => {
        const clipboardData = e.clipboardData || window.clipboardData;
        const pastedData = parseFloat(clipboardData.getData('text'));

        if (pastedData < 0) {
            e.preventDefault();
        }
    };

    const preventMinusKeyPress = (e) => {
        if (e.key === '-') {
            e.preventDefault();
        }
    };

    const handleFormChange = (event, index) => {
        if (event.target.name == 'positiveAmount' && event.target.value.includes('-')) console.log("menos ");

        let data = [...formFields];
        data[index][event.target.name] = event.target.value;
        setFormFields(data);
        props.transaction.transactionAmounts = formFields;
        props.updateTotalAmount();
    }

    const submit = (e) => {
        e.preventDefault();
        console.log(formFields)
    }

    const addFields = (e) => {
        e.preventDefault();

        if (formFields.length == users.length) {
            alert('the number of transaction amounts cannot be bigger the the list of user')
            return;
        }

        let object = {
            id : '0',
            userId: '0',
            positiveAmount: ''
        }

        setFormFields([...formFields, object])
    }

    const removeFields = (e, index) => {
        e.preventDefault();
        let data = [...formFields];
        data.splice(index, 1)
        setFormFields(data)
        props.transaction.transactionAmounts = formFields;
    }

    if (formFields == null) return "Loading";
    return (
        <div className="TransactionAmountsForm" >

            <button className='btn btn-success float-end fa-plus  me-2' id="addNewTransactionBtn" onClick={addFields}> Add</button>

            <table className='table table-striped table-hover align-middle'>
                <thead className='table-dark mt-3 align-middle'>
                    <tr>
                        <th scope='col' className="text-center">User</th>
                        <th scope='col' className="text-center">Amount</th>
                        <th scope='col' className="text-center">Options</th>
                    </tr>
                </thead>
                <tbody>
                    {formFields.map((form, index) => {
                        return (
                            <tr key={index}>
                                <td>
                                    <select
                                        name='userId'
                                        value={form.userId}
                                        onChange={event => handleFormChange(event, index)}
                                        id='userId'
                                        className="form-select form-select-md"
                                    >
                                        <option key='0'  value='0'>Select...</option>
                                        {users.map((user) => (
                                            <option key={user.id} value={user.id}>{user.name}</option>
                                        ))}
                                    </select>
                                </td>

                                <td>
                                    <input
                                        name='positiveAmount'
                                        placeholder='positive amount'
                                        onChange={event => handleFormChange(event, index)}
                                        onKeyPress={preventMinusKeyPress}
                                        onPaste={preventPasteNegative}
                                        value={form.positiveAmount}
                                        className="form-control"
                                        type="number"
                                        required 
                                    />
                                </td>
                                <td >
                                    <button
                                        className='btn btn-danger '
                                        onClick={(e) => removeFields(e, index)}
                                        disabled={formFields.length <= 1}
                                    >
                                        Remove
                                    </button>
                                </td>
                            </tr>
                        )
                    })}

                </tbody>
            </table>         

        </div>
    );
}