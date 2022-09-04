import React from 'react';
import Button from 'react-bootstrap/Button';
import Badge from 'react-bootstrap/Badge';

export default function TransactionItem(props) {

    const getAmountColor = () => {
        if (String.fromCharCode(props.transaction.flow) == 'c')
            return { color: "red" };
        else
            return { color: "green" }
    };

    const getAmountIcon = () => {
        if (String.fromCharCode(props.transaction.flow) == 'c')
            return 'fa-sharp fa-solid fa-caret-down'
        else
            return 'fa-sharp fa-solid fa-caret-up'
    };

    return (

        <tr key={props.transaction.id}>
            <td>{props.transaction.shortDescription}</td>
            <td>{new Date(props.transaction.date).toLocaleDateString()}</td>
            <td>
                {
                    <h5>
                        <Badge ref={element => {
                            if (element) {
                                element.style.setProperty('background-color', props.transaction.transactionType.argbColor, 'important');
                            }
                        }}>
                            {props.transaction.transactionType.name}
                        </Badge>
                    </h5>
                }
            </td>
            <td>{props.transaction.user.name}</td>
            <td>
                {
                    String.fromCharCode(props.transaction.flow) == 'c' ?
                        (
                            <Button className='btn btn-sm btn-primary me-2' disabled>
                                <i className='fa-sharp fa-solid fa-arrow-down'></i>
                            </Button>
                        ) :
                        (
                            <Button className='btn btn-sm btn-success me-2' disabled>
                                <i className='fa-sharp fa-solid fa-arrow-up'></i>
                            </Button>
                        )
                }
            </td>
            <td>
                <div text-align='right'>
                    <i className={getAmountIcon()} style={getAmountColor()}></i>
                    <a style={getAmountColor()}>
                        {"$" + props.transaction.totalAmount.toFixed(2)}
                    </a>
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
