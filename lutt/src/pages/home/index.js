import axios from 'axios';
import { useState } from 'react';
import { Button, Col, Row, Spinner } from 'react-bootstrap';
import pass from '../../mock/pass';
import InputForm from './InputForm';
import ListCV from './ListCV';
import ListPass from './ListPass';

const Home = () => {
  const [files, setFiles] = useState([]);
  const [state, setState] = useState({});
  const [loading, setLoading] = useState(false);

  const onSubmit = async () => {
    setLoading(true);
    setTimeout(() => {
      setLoading(false);
    }, 2000);
    try {
      const formData = new FormData();

      // Update the formData object
      formData.append('info', JSON.stringify(state));

      files.forEach((file, index) => {
        formData.append(`file${index}`, file);
      });

      // Details of the uploaded file
      console.log(formData);
      const test = await axios.post('http://192.168.10.35:5001/', formData);
      console.log(test);
    } catch (error) {
      console.log('----', error);
    }
  };

  return (
    <div style={{ padding: '40px 0' }}>
      <Row>
        <Col xs lg="6">
          <InputForm setFiles={setFiles} state={state} setState={setState} />
        </Col>
        <Col xs lg="6">
          {!!files.length && <ListCV data={files} />}
        </Col>
      </Row>

      <Row className="justify-content-center">
        <Col xs lg="2">
          <div className="d-grid gap-2">
            <Button
              block
              variant="success"
              // size="lg"
              onClick={e => {
                e.preventDefault();
                onSubmit();
              }}
              disabled={
                Object.keys(state).length < 3 || !files.length || loading
              }
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

      <ListPass data={pass} />
    </div>
  );
};
export default Home;
