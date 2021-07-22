import { Col, Container, Row } from 'react-bootstrap';
import './App.scss';
import Home from './pages/home';
import 'bootstrap/dist/css/bootstrap.min.css';

function App() {
  return (
    <Container>
      <Row style={{ minHeight: '100vh' }} className="justify-content-center">
        <Col style={{ background: 'primary' }} xs lg="10">
          <Home />
        </Col>
      </Row>
    </Container>
  );
}

export default App;
