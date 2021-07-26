import { Viewer } from '@react-pdf-viewer/core';
import axios from 'axios';
import { useState } from 'react';
import { Button, Col, Modal, Row, Spinner } from 'react-bootstrap';
import languages from '../../mock/languages';
import InputForm from './InputForm';
import ListCV from './ListCV';
import ListPass from './ListPass';

const Home = () => {
  const [files, setFiles] = useState([]);
  const [state, setState] = useState({});
  const [loading, setLoading] = useState(false);
  const [url, setUrl] = useState();
  const [error, setError] = useState();
  const [cvPass, setCvPass] = useState();

  const onSubmit = async () => {
    try {
      setLoading(true);
      setCvPass();
      const formData = new FormData();

      // Update the formData object
      formData.append('info', JSON.stringify(state));

      Array.from(files).forEach((file, index) => {
        formData.append(`file${index}`, file);
      });
      // Details of the uploaded file
      const res = await axios.post('http://192.168.10.35:5001/', formData);
      setCvPass(res.data.filter(({ pass }) => pass));
    } catch (error) {
      setError(error.message);
    } finally {
      setLoading(false);
    }
  };

  const handleClose = () => {
    setError(undefined);
  };
  const isNotValidateForm = () => {
    if (languages[state.position]) {
      return (
        Object.values(state).filter(item => !!item).length < 3 || !files.length
      );
    }
    return (
      Object.values(state).filter(item => !!item).length < 2 || !files.length
    );
  };

  return (
    <div style={{ padding: '40px 0' }}>
      {cvPass && <ListPass data={cvPass} />}

      <Row>
        <Col xs lg="6">
          <InputForm
            setFiles={v => {
              setFiles(v);
              setUrl(undefined);
            }}
            state={state}
            setState={setState}
          />
        </Col>
        <Col xs lg="6">
          {!!files.length ? (
            <ListCV
              data={files}
              setUrl={setUrl}
              removeCV={index => {
                if (files[index]) {
                  setFiles(files.filter((cv, i) => i !== index));
                }
              }}
            />
          ) : (
            <Row
              style={{
                margin: 0,
                textAlign: 'center',
                border: '1px solid',
                borderColor: '#FFB1B1',
                borderRadius: 4,
                height: '100%'
              }}
            >
              <Col style={{ alignSelf: 'center' }}>
                <p style={{ margin: 0 }}>LIST CV WILL SHOW HERE</p>
              </Col>
            </Row>
          )}
        </Col>
      </Row>
      <Row className="justify-content-center">
        <Col xs lg="2">
          <div className="d-grid gap-2">
            <Button
              className="mt-5"
              block
              variant="primary"
              onClick={e => {
                e.preventDefault();
                onSubmit();
              }}
              disabled={isNotValidateForm() || loading}
            >
              {loading && (
                <Spinner
                  as="span"
                  animation="grow"
                  size="sm"
                  role="status"
                  aria-hidden="true"
                  style={{ marginRight: 14 }}
                />
              )}
              {loading ? 'Loading...' : 'Submit'}
            </Button>
          </div>
        </Col>
      </Row>

      {url && (
        <div
          style={{
            marginTop: 40,
            border: '1px solid rgba(0, 0, 0, 0.3)',
            height: '100%'
          }}
        >
          <Viewer fileUrl={url} />
        </div>
      )}

      <Modal show={!!error} onHide={handleClose} centered>
        <Modal.Header>
          <Modal.Title>Error</Modal.Title>
        </Modal.Header>
        <Modal.Body
          style={{ color: 'red', textAlign: 'center', padding: '50px 0' }}
        >
          {error}
        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={handleClose}>
            Close
          </Button>
        </Modal.Footer>
      </Modal>
    </div>
  );
};
export default Home;
