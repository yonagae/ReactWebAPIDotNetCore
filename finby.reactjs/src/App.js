import './App.css';
import Transaction from './pages/transactions/Transaction';
import { Routes, Route } from 'react-router-dom';
import Cliente from './pages/clientes/Cliente';
import ClienteForm from './pages/clientes/ClienteForm';
import Dashboard from './pages/dashboard/Dashboard';
import PageNotFound from './pages/PageNotFound';
import TransacationType from './pages/transacationType/TransacationType'

export default function App() {
    return (
        <Routes>
            <Route path='/' element={<Dashboard />} />
            <Route path='/transaction/*' element={<Transaction />} />
            <Route path='/transaction/:id/cliente' element={<Cliente />} />
            <Route path='/cliente/*' element={<Cliente />} />
            <Route path='/cliente/:id/transaction' element={<Transaction />} />
            <Route path='/cliente/detalhe/' element={<ClienteForm />} />
            <Route path='/cliente/detalhe/:id' element={<ClienteForm />} />
            <Route path='/transactionType/*' element={<TransacationType />} />            
            <Route element={<PageNotFound />} />
        </Routes>
    );
}
