import React from 'react';
import { useState, useEffect } from 'react';
import TitlePage from '../../components/TitlePage';
import { Card, Row, Col } from 'react-bootstrap';
import api from '../../api/transaction';
import DatePicker from "react-datepicker";
import CustomLabelPieChart from '../../components/CustomLabelPieChart'

export default function Dashboard() {
    const [sumByTransactionType, setSumByTransactionType] = useState([]);
    const [sumByUser, sumByUserType] = useState([]);
    const [startDate, setStartDate] = useState(new Date(2022, 0, 1));
    const [endDate, setEndDate] = useState(new Date(2022, 1, 1));

    const getGetSumOfTransactionsByTypeByPeriod = async () => {
        try {
            const response = await api.get('transactions/sumExpenseByType', { params: { start: startDate.toLocaleDateString('en-CA'), end: endDate.toLocaleDateString('en-CA') } });
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

    const getGetSumOfTransactionsByUserByPeriod = async () => {
        try {
            const response = await api.get('transactions/userbalance', { params: { start: startDate.toLocaleDateString('en-CA'), end: endDate.toLocaleDateString('en-CA') } });
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
        const getTransactions = async () => {
            const todasTransactions = await getGetSumOfTransactionsByTypeByPeriod();

            if (todasTransactions)
                setSumByTransactionType(todasTransactions);
        };
        getTransactions();

        const getTransactionsByUser = async () => {
            const transactionsSumByUser = await getGetSumOfTransactionsByUserByPeriod();

            if (transactionsSumByUser)
                sumByUserType(transactionsSumByUser);
        };
        getTransactionsByUser();

    }, endDate);

    const onDatePickerChange = (dates) => {
        const [start, end] = dates;
        setStartDate(start);
        setEndDate(end);
    };

    const fillPieData = () => {
        let pieChartData = [];

        sumByTransactionType.forEach(element => {
            pieChartData.push({ name: element.transactionType.name, value: element.sum, argbColor: element.transactionType.argbColor });
        });

        return pieChartData;
    }


    return (
        <>
            <TitlePage title='Dashboard' />
            <div className="row">
                <div className='col-sm-12'>
                    <label className='form-label'>Pick a data range:</label>
                </div>
                <div className='col-sm-12'>
                    <DatePicker
                        onChange={onDatePickerChange}
                        startDate={startDate}
                        endDate={endDate}
                        dateFormat="dd/MM/yyyy"
                        wrapperClassName="date-picker"
                        className='form-control col-sm-6'
                        id='datePicker'
                        selectsRange
                    />
                </div>
            </div>

            <Row>
                <Col>
                    <CustomLabelPieChart
                        fillPieData={fillPieData}
                    />
                </Col>

                <Col>
                    <div className='mt-3'>
                        <Row>
                            {sumByUser.map((st, index) => (
                                <Col key={index}>
                                    <Card border='dark'>
                                        <Card.Header>{st.userName}</Card.Header>
                                        <Card.Body>
                                            <Card.Title>
                                                <h1 className='text-center' style={{ color: "green" }}>
                                                    <i className='fa-sharp fa-solid fa-caret-up' style={{ color: "green" }}></i>
                                                    {
                                                        st.sumByFlowType.Debit != null ? (
                                                            '$' + st.sumByFlowType.Debit.toFixed(2)
                                                        ) : (
                                                            '$ 0.00'
                                                        )
                                                    }
                                                </h1>
                                                <h1 className='text-center' style={{ color: "red" }}>
                                                    <i className='fa-sharp fa-solid fa-caret-down' style={{ color: "red" }}></i>
                                                    {
                                                        st.sumByFlowType.Credit != null ? (
                                                            '$' + st.sumByFlowType.Credit.toFixed(2)
                                                        ) : (
                                                            '$ 0.00'
                                                        )
                                                    }
                                                </h1>
                                            </Card.Title>
                                        </Card.Body>
                                    </Card>
                                </Col>
                            ))}
                        </Row>
                    </div>
                </Col>
            </Row>


            <div className='mt-3'>
                <Row fluid='true'>
                    {sumByTransactionType.map((st, index) => (
                        <Col key={index}>
                            <Card border='success' style={{ width: '12rem' }}>
                                <Card.Header style={{ backgroundColor: st.transactionType.argbColor, color: 'white', fontSize : 'large'}}>{st.transactionType.name}</Card.Header>
                                <Card.Body>
                                    <Card.Title>
                                        <h1 className='text-center'>${st.sum.toFixed(2)}</h1>
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
