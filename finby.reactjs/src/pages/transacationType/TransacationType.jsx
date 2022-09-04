import { useState, useEffect } from 'react';
import { Button, Modal } from 'react-bootstrap';
import TransacationTypeForm from './TransacationTypeForm';
import TransacationTypeLista from './TransacationTypeLista';
import api from '../../api/transaction';
import TitlePage from '../../components/TitlePage';

export default function Atividade() {
    const [showAtividadeModal, setShowAtividadeModal] = useState(false);
    const [smShowConfirmModal, setSmShowConfirmModal] = useState(false);

    const [transactionTypes, setAtividades] = useState([]);
    const [atividade, setAtividade] = useState({ id: 0 });

    const handleAtiviadeModal = () =>
        setShowAtividadeModal(!showAtividadeModal);

    const handleConfirmModal = (id) => {
        if (id !== 0 && id !== undefined) {
            const atividade = transactionTypes.filter(
                (atividade) => atividade.id === id
            );
            setAtividade(atividade[0]);
        } else {
            setAtividade({ id: 0 });
        }
        setSmShowConfirmModal(!smShowConfirmModal);
    };

    const pegaTodasAtividades = async () => {
        const response = await api.get('transactionTypes');
        return response.data;
    };

    const novaAtividade = () => {
        setAtividade({ id: 0 });
        handleAtiviadeModal();
    };

    useEffect(() => {
        const getAtividades = async () => {
            const todasAtividades = await pegaTodasAtividades();
            if (todasAtividades) setAtividades(todasAtividades);
        };
        getAtividades();
    }, []);

    const addAtividade = async (ativ) => {
        handleAtiviadeModal();
        const response = await api.post('transactionTypes', ativ);
        console.log(response.data);
        setAtividades([...transactionTypes, response.data]);
    };

    const cancelarAtividade = () => {
        setAtividade({ id: 0 });
        handleAtiviadeModal();
    };

    const atualizarAtividade = async (ativ) => {
        handleAtiviadeModal();
        const response = await api.put(`transactionTypes/${ativ.id}`, ativ);
        const { id } = response.data;
        setAtividades(
            transactionTypes.map((item) => (item.id === id ? response.data : item))
        );
        setAtividade({ id: 0 });
    };

    const deletarAtividade = async (id) => {
        handleConfirmModal(0);
        if (await api.delete(`transactionTypes/${id}`)) {
            const atividadesFiltradas = transactionTypes.filter(
                (atividade) => atividade.id !== id
            );
            setAtividades([...atividadesFiltradas]);
        }
    };

    const pegarAtividade = (id) => {
        const atividade = transactionTypes.filter((atividade) => atividade.id === id);
        setAtividade(atividade[0]);
        handleAtiviadeModal();
    };

    return (
        <>
            <TitlePage
                title={'Atividade ' + (atividade.id !== 0 ? atividade.id : '')}
            >
                <Button variant='outline-secondary' onClick={novaAtividade}>
                    <i className='fas fa-plus'></i>
                </Button>
            </TitlePage>

            <TransacationTypeLista
                transactionTypes={transactionTypes}
                pegarAtividade={pegarAtividade}
                handleConfirmModal={handleConfirmModal}
            />

            <Modal show={showAtividadeModal} onHide={handleAtiviadeModal}>
                <Modal.Header closeButton>
                    <Modal.Title>
                        Atividade {atividade.id !== 0 ? atividade.id : ''}
                    </Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <TransacationTypeForm
                        addAtividade={addAtividade}
                        cancelarAtividade={cancelarAtividade}
                        atualizarAtividade={atualizarAtividade}
                        ativSelecionada={atividade}
                        transactionTypes={transactionTypes}
                    />
                </Modal.Body>
            </Modal>

            <Modal
                size='sm'
                show={smShowConfirmModal}
                onHide={handleConfirmModal}
            >
                <Modal.Header closeButton>
                    <Modal.Title>
                        Excluindo Atividade{' '}
                        {atividade.id !== 0 ? atividade.id : ''}
                    </Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    Tem certeza que deseja Excluir a Atividade {atividade.id}
                </Modal.Body>
                <Modal.Footer className='d-flex justify-content-between'>
                    <button
                        className='btn btn-outline-success me-2'
                        onClick={() => deletarAtividade(atividade.id)}
                    >
                        <i className='fas fa-check me-2'></i>
                        Sim
                    </button>
                    <button
                        className='btn btn-danger me-2'
                        onClick={() => handleConfirmModal(0)}
                    >
                        <i className='fas fa-times me-2'></i>
                        Não
                    </button>
                </Modal.Footer>
            </Modal>
        </>
    );
}
