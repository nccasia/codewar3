import { Col, Container, Row } from 'react-bootstrap';
import './App.scss';
import Home from './pages/home';
// import 'bootstrap/dist/css/bootstrap.min.css';
import { Header } from './components';
import { Worker } from '@react-pdf-viewer/core';

function App() {
  return (
    <>
      <Worker workerUrl="https://unpkg.com/pdfjs-dist@2.6.347/build/pdf.worker.min.js">
        <Container style={{ minHeight: '100vh' }}>
          <Header />
          <Row className="justify-content-center">
            <Col style={{ background: 'primary' }} xs lg="10">
              <Home />
            </Col>
          </Row>
        </Container>
      </Worker>
    </>
  );
}

export default App;
