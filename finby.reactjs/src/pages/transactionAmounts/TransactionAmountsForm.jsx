import { useState, useEffect } from 'react';
import api from '../../api/transaction';

export default function TransactionAmountsForm(props) {
    const [formFields, setFormFields] = useState([
        { userid: '0', amount: '' },
    ])
    const [users, setUsers] = useState([]);

    useEffect(() => {
        getAllUsers().then(result => setUsers(result));
    }, []);

    async function getAllUsers() {
        const response = await api.get('users');
        return response.data;
    };

    const handleFormChange = (event, index) => {
        let data = [...formFields];
        data[index][event.target.name] = event.target.value;
        setFormFields(data);
        props.transaction.amounts = formFields;
    }

    const submit = (e) => {
        e.preventDefault();
        console.log(formFields)
    }

    const addFields = (e) => {
        e.preventDefault();

        let object = {
            userid: '0',
            amount: ''
        }

        setFormFields([...formFields, object])
    }

    const removeFields = (e, index) => {
        e.preventDefault();
        let data = [...formFields];
        data.splice(index, 1)
        setFormFields(data)
    }


    return (
        <div className="TransactionAmountsForm" >

            <button className='btn btn-success float-end' onClick={addFields}>Add More</button>

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
                                        name='userid'
                                        value={form.id}
                                        defaultValue={0}
                                        onChange={event => handleFormChange(event, index)}
                                        id='userid'
                                        className="form-select form-select-md"
                                    >
                                        <option key='0'  value='0'>Selecione...</option>
                                        {users.map((user) => (
                                            <option key={user.id} value={user.id}>{user.name}</option>
                                        ))}

                                    </select>
                                </td>
                                <td>
                                    <input
                                        name='amount'
                                        placeholder='amount'
                                        onChange={event => handleFormChange(event, index)}
                                        value={form.amount}
                                        className="form-control"
                                        type="number"
                                    />
                                </td>
                                <td >
                                    <button
                                        className='btn btn-danger'
                                        onClick={(e) => removeFields(e, index)}
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