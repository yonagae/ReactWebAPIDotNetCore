import React from 'react';
import { useState, useEffect } from 'react';
import TitlePage from '../../components/TitlePage';
import { Card, Row, Col } from 'react-bootstrap';
import api from '../../api/transaction';
import DatePicker from "react-datepicker";
import CustomLabelPieChart from '../../components/CustomLabelPieChart'

export default function Dashboard() {
    const [sumByTransactionType, setSumByTransactionType] = useState([]);
    const [startDate, setStartDate] = useState(new Date(2022, 0, 1));
    const [endDate, setEndDate] = useState(new Date(2022, 1, 1));

    const getGetSumOfTransactionsByTypeByPeriod = async () => {
        const response = await api.get('transactions/dashboard', { params: { start: startDate.toLocaleDateString('en-CA'), end: endDate.toLocaleDateString('en-CA') } });
        return response.data;
    };

    useEffect(() => {
        const getTransactions = async () => {
            const todasTransactions = await getGetSumOfTransactionsByTypeByPeriod();

            if (todasTransactions)
                setSumByTransactionType(todasTransactions);
        };
        getTransactions();
    }, endDate);

    const onDatePickerChange = (dates) => {
        const [start, end] = dates;
        setStartDate(start);
        setEndDate(end);
    };

    const fillPieData = () => {
        let pieChartData = [];
  
        sumByTransactionType.forEach(element => {            
            pieChartData.push({ name: element.item1.name, value: element.item2 });
        });

        return pieChartData;
    }


    return (
        <>
            <TitlePage title='Dashboard' />

            <DatePicker
                onChange={onDatePickerChange}
                startDate={startDate}
                endDate={endDate}
                dateFormat="dd/MM/yyyy"
                wrapperClassName="date-picker"
                className='form-control'
                id='datePicker'
                selectsRange

            />
            <CustomLabelPieChart
                fillPieData={fillPieData}
            />
            <div className='mt-3'>
                <Row>
                    {sumByTransactionType.map((st, index) => (
                        <Col key={index}>
                            <Card border='success'>
                                <Card.Header>{st.item1.name}</Card.Header>
                                <Card.Body>
                                    <Card.Title>
                                        <h1 className='text-center'>${st.item2.toFixed(2)}</h1>
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
