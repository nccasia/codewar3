import { Table } from 'react-bootstrap';

const ListPass = ({ data }) => {
  return (
    <Table style={{ marginTop: '30px' }} striped bordered hover>
      <thead>
        <tr>
          <th>#</th>
          <th>Name</th>
          <th>age</th>
          <th>Gender</th>
          <th>Phone</th>
        </tr>
      </thead>
      <tbody>
        {data?.map((item, index) => (
          <tr key={index}>
            <td>{index + 1}</td>
            <td>{item.name}</td>
            <td>{item.age}</td>
            <td>{item.gender}</td>
            <td>{item.phone}</td>
          </tr>
        ))}
      </tbody>
    </Table>
  );
};

export default ListPass;
