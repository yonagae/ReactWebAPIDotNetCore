import React from 'react';
import TitlePage from '../../components/TitlePage';
import { Card, Row, Col } from 'react-bootstrap';

export default function Dashboard() {
    return (
        <>
            <TitlePage title='Dashboard' />
            <div className='mt-3'>
                <Row>
                    <Col>
                        <Card border='success'>
                            <Card.Header>Clientes atuais</Card.Header>
                            <Card.Body>
                                <Card.Title>
                                    <h1 className='text-center'>25</h1>
                                </Card.Title>
                            </Card.Body>
                        </Card>
                    </Col>
                    <Col>
                        <Card border='secondary'>
                            <Card.Header bg={'danger'}>
                                Transactions totais
                            </Card.Header>
                            <Card.Body>
                                <Card.Title>
                                    <h1 className='text-center'>256</h1>
                                </Card.Title>
                            </Card.Body>
                        </Card>
                    </Col>
                    <Col>
                        <Card border='warning'>
                            <Card.Header>Transactions Urgentes</Card.Header>
                            <Card.Body>
                                <Card.Title>
                                    <h1 className='text-center'>25</h1>
                                </Card.Title>
                            </Card.Body>
                        </Card>
                    </Col>
                    <Col>
                        <Card border='danger'>
                            <Card.Header>Transactions Atrasadas</Card.Header>
                            <Card.Body>
                                <Card.Title>
                                    <h1 className='text-center'>2</h1>
                                </Card.Title>
                            </Card.Body>
                        </Card>
                    </Col>
                </Row>
            </div>
        </>
    );
}
