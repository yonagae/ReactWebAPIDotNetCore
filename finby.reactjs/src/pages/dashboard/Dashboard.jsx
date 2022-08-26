import React from 'react';
import { useState, useEffect } from 'react';
import TitlePage from '../../components/TitlePage';
import { Card, Row, Col } from 'react-bootstrap';
import api from '../../api/transaction';

export default function Dashboard() {
    const [sumByTransactionType, setSumByTransactionType] = useState([]);

    const getGetSumOfTransactionsByType = async () => {
        const response = await api.get('transactions/dashboard');
        return response.data;
    };

    useEffect(() => {
        const getTransactions = async () => {
            const todasTransactions = await getGetSumOfTransactionsByType();
            if (todasTransactions) setSumByTransactionType(todasTransactions);
        };
        getTransactions();
    }, []);


    return (
        <>
            <TitlePage title='Dashboard' />
            <div className='mt-3'>
                <Row>
                    {sumByTransactionType.map((st, index) => (
                        <Col key={index}>
                            <Card border='success'>
                                <Card.Header>{st.item1.name}</Card.Header>
                                <Card.Body>
                                    <Card.Title>
                                        <h1 className='text-center'>{st.item2}</h1>
                                    </Card.Title>
                                </Card.Body>
                            </Card>
                        </Col>
                    ))}                   
                </Row>
            </div>
        </>
    );
}
