import { useState } from 'react';

export default function TransactionAmountsForm(props) {
    const [formFields, setFormFields] = useState([
        { name: '', amount: '' },
    ])

    const handleFormChange = (event, index) => {
        let data = [...formFields];
        data[index][event.target.name] = event.target.value;
        setFormFields(data);
    }

    const submit = (e) => {
        e.preventDefault();
        console.log(formFields)
    }

    const addFields = () => {
        let object = {
            name: '',
            amount: ''
        }

        setFormFields([...formFields, object])
    }

    const removeFields = (index) => {
        let data = [...formFields];
        data.splice(index, 1)
        setFormFields(data)
    }

    return (
        <div className="TransactionAmountsForm" >
            {formFields.map((form, index) => {
                return (
                    <div key={index}>
                        <input
                            name='name'
                            placeholder='Name'
                            onChange={event => handleFormChange(event, index)}
                            value={form.name}
                        />
                        <input
                            name='amount'
                            placeholder='amount'
                            onChange={event => handleFormChange(event, index)}
                            value={form.amount}
                        />
                        <button onClick={() => removeFields(index)}>Remove</button>
                    </div>
                )
            })}
            <button onClick={addFields}>Add More..</button>
            <br />
            <button onClick={submit}>Submit</button>
        </div>
    );
}