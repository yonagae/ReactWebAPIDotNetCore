import { Navbar, Container, Nav, NavDropdown } from 'react-bootstrap';
import { NavLink } from 'react-router-dom';

export default function Menu() {
    return (
        <Navbar bg='dark' expand='lg' variant='dark'>
            <Container>
                <Navbar.Brand as={NavLink} to='/'>
                    Ativy
                </Navbar.Brand>
                <Navbar.Toggle aria-controls='basic-navbar-nav' id="toggle" />
                <Navbar.Collapse id='basic-navbar-nav'>
                    <Nav className='me-auto'>
                        <Nav.Link
                            className={(navData) => navData.isActive ? 'Active' : ''}
                            as={NavLink}
                            to='/cliente/lista'
                        >
                            Clientes
                        </Nav.Link>
                        <Nav.Link
                            id="menuTransaction"
                            className={(navData) => navData.isActive ? 'Active' : ''}
                            as={NavLink}
                            to='/transaction/lista'
                        >
                            Transactions
                        </Nav.Link>

                        <Nav.Link
                            className={(navData) => navData.isActive ? 'Active' : ''}
                            as={NavLink}
                            to='/transactionType/lista'
                        >
                            Transaction Types
                        </Nav.Link>

                    </Nav>
                    <Nav>
                        <NavDropdown
                            align='end'
                            title='Vinícius'
                            id='basic-nav-dropdown'
                        >
                            <NavDropdown.Item href='#action/3.1'>
                                Perfil
                            </NavDropdown.Item>
                            <NavDropdown.Item href='#action/3.3'>
                                Configurações
                            </NavDropdown.Item>
                            <NavDropdown.Divider />
                            <NavDropdown.Item href='#action/3.4'>
                                Sair
                            </NavDropdown.Item>
                        </NavDropdown>
                    </Nav>
                </Navbar.Collapse>
            </Container>
        </Navbar>
    );
}
