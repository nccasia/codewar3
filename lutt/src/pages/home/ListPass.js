import { Alert, Table } from 'react-bootstrap';

const ListPass = ({ data }) => {
  return (
    <div style={{ marginBottom: 60 }}>
      {!data.length ? (
        <Alert
          variant="primary"
          style={{ margin: 0, padding: 50, textAlign: 'center' }}
        >
          NO CV PASSED
        </Alert>
      ) : (
        <>
          <h2 style={{ textAlign: 'center' }}>LIST CV PASSED</h2>
          <Table style={{ marginTop: '30px' }} striped bordered hover>
            <thead>
              <tr>
                <th>#</th>
                <th>Name</th>
                <th>Phone</th>
                <th>Experiences</th>
                <th>File name</th>
                <th>Email</th>
                <th>Matching (%)</th>
              </tr>
            </thead>
            <tbody>
              {data?.map((item, index) => (
                <tr key={index}>
                  <td>{index + 1}</td>
                  <td>{item.name || '-'}</td>
                  <td>{item.phone || '-'}</td>
                  <td>{item.experiences || '-'}</td>
                  <td>{item.file || '-'}</td>
                  <td>{item.mail || '-'}</td>
                  <td>{item.matching}</td>
                </tr>
              ))}
            </tbody>
          </Table>
        </>
      )}
    </div>
  );
};

export default ListPass;
