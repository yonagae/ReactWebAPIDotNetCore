import { BarChart, Bar, Cell, XAxis, YAxis, CartesianGrid, Tooltip, Legend, ResponsiveContainer } from 'recharts';
import { useState, useEffect } from 'react';
import api from '../../api/transaction';
import DatePicker from "react-datepicker";
import Select from 'react-select';

export default function UserExpense(prop) {

    const [startDate, setStartDate] = useState(new Date(2022, 0, 1));
    const [endDate, setEndDate] = useState(new Date(2024, 1, 1));
    const [yearlyExpenseData, setyearlyExpenseData] = useState([]);
    const [transactionTypes, setTransactionTypes] = useState([]);
    const [selectedTransactionTypes, setSelectedTransactionTypes] = useState([]);

    useEffect(() => {
        const getTransactions = async () => {
            const todasTransactions = await getYearlyExpenseByPeriod();

            if (todasTransactions)
                setyearlyExpenseData(todasTransactions);
        };
        getTransactions();
    }, [endDate, selectedTransactionTypes]);

    useEffect(() => {
        getTransactionTypes().then(result => setTransactionTypes(result));
    }, []);

    async function getTransactionTypes() {
        try {
            const response = await api.get('transactionTypes');
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

    const onDatePickerChange = (dates) => {
        const [start, end] = dates;
        setStartDate(start);
        setEndDate(end);
    };

    const getYearlyExpenseByPeriod = async () => {
        try {
            const response = await api.get('transactions/monthlyExpenseByPeriodAndUser', {
                params:
                {
                    userId: 1,
                    start: startDate.toLocaleDateString('en-CA'),
                    end: endDate.toLocaleDateString('en-CA'),
                    listOfTransactionsTypeIds: selectedTransactionTypes.map(a => a.value)
                }
            });
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

    const getSelectTypesOptions = () => {
        var selectTypesOptions = [];
        transactionTypes.forEach(function (elemento) {
            selectTypesOptions.push({ value: elemento.id, label: elemento.name });
        });
        return selectTypesOptions;
    };


    const onInputChange = (entry) => {
        setSelectedTransactionTypes(entry);
    }

    return (
        <>
            <div className="row">
                <div className='col-sm-12'>
                    <label className='form-label'>Pick a data range:</label>
                </div>
                <div className='col-sm-12'>
                    <DatePicker
                        onChange={onDatePickerChange}
                        startDate={startDate}
                        endDate={endDate}
                        dateFormat="MM/yyyy"
                        wrapperClassName="date-picker"
                        className='form-control col-sm-6'
                        id='datePicker'
                        selectsRange
                        showMonthYearPicker
                    />
                </div>
                <div className='col-sm-12'>
                    <Select
                        defaultValue={getSelectTypesOptions()[0]}
                        isMulti
                        name="colors"
                        options={getSelectTypesOptions()}
                        className="basic-multi-select"
                        classNamePrefix="select"
                        onChange={onInputChange}
                    />
                </div>
            </div>
            <br /><br /><br />
            <div className="row">
                <div className='col-sm-12'>
                    <ResponsiveContainer width="100%" height={400}>
                        <BarChart
                            height={300}
                            data={yearlyExpenseData}
                            margin={{
                                top: 5,
                                right: 30,
                                left: 20,
                                bottom: 5,
                            }}
                        >
                            <CartesianGrid strokeDasharray="3 3" />
                            <XAxis dataKey="name" />
                            <YAxis />
                            <Tooltip />
                            <Legend />
                            {transactionTypes.map((t) =>
                                selectedTransactionTypes.some(e => e.label == t.name) ?
                                    (<Bar dataKey={t.name} fill={t.argbColor} />)
                                    : ('')
                            )}
                        </BarChart>
                    </ResponsiveContainer>
                </div>
            </div>

            <div className='mt-3'>
                <table id="transactionsTable" className='table table-striped table-hover'>
                    <thead className='table-dark mt-3'>
                        <tr>
                            <th scope='col'>
                                Date
                            </th>

                            {transactionTypes.map((t) =>
                                selectedTransactionTypes.some(e => e.label == t.name) ?
                                    (
                                        <th scope='col'>
                                            {t.name}
                                        </th>
                                    )
                                    : ('')
                            )}
                        </tr>
                    </thead>
                    <tbody>

                        {yearlyExpenseData.map((monthExpense) =>
                            <tr key={1}>
                                < td > {monthExpense.name}</td>
                                {
                                    transactionTypes.map((t) =>
                                        selectedTransactionTypes.some(e => e.label == t.name) ?
                                            (< td > {monthExpense[t.name]}</td>)
                                            : ('')
                                    )}
                            </tr>
                        )}

                    </tbody>
                </table>
            </div>
        </>
    );
}
