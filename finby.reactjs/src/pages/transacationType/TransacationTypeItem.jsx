import React from 'react';
import Badge from 'react-bootstrap/Badge';
//import Button from 'react-bootstrap/Button';

export default function TransacationTypeItem(props) {

    return (

        <tr key={props.ativ.id}>
            <td>{props.ativ.name}</td>
            <td>              
                <Badge  ref={element => {
                    if (element) {
                        element.style.setProperty('background-color', props.ativ.argbColor, 'important');
                    }
                }}>
                    {props.ativ.argbColor}
                </Badge>
            </td>
            <td>
                <button
                    className='btn btn-sm btn-outline-primary me-2'
                    onClick={() => props.pegarAtividade(props.ativ.id)}
                >
                    <i className='fas fa-pen me-2'></i>
                    Editar
                </button>
                <button
                    className='btn btn-sm btn-outline-danger'
                    onClick={() => props.handleConfirmModal(props.ativ.id)}
                >
                    <i className='fas fa-trash me-2'></i>
                    Deletar
                </button>
            </td>
        </tr>
    );
}
