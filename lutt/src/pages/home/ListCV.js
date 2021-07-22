import { useState } from 'react';
import { ListGroup, Tab } from 'react-bootstrap';

const ListCV = ({ data }) => {
  const [active, setActive] = useState(-1);

  return (
    <Tab.Container>
      {/* <Row> */}
      {/* <Col sm={4}> */}
      <ListGroup>
        {data?.map((cv, index) => (
          <ListGroup.Item
            key={index}
            action
            onClick={() => {
              setActive(index);
            }}
            active={active === index}
          >
            {cv.name}
          </ListGroup.Item>
        ))}
      </ListGroup>
      {/* </Col> */}
      {/* <Col sm={8}>
          <Tab.Content>
            <p>lorem</p>
          </Tab.Content>
        </Col> */}
      {/* </Row> */}
    </Tab.Container>
  );
};

export default ListCV;
