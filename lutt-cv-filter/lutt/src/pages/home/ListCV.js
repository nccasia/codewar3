import { useEffect, useState } from 'react';
import { Alert, ListGroup, Tab } from 'react-bootstrap';

const ListCV = ({ data, setUrl, removeCV }) => {
  const [active, setActive] = useState(-1);

  useEffect(() => {
    setActive(-1);
  }, [data]);

  return (
    <div
      style={{
        border: 'solid 1px',
        borderColor: '#FFB1B1',
        borderRadius: '.25rem'
      }}
    >
      <Alert variant="primary" style={{ margin: 0, textAlign: 'center' }}>
        List of CV
      </Alert>
      <Tab.Container>
        <ListGroup>
          {data?.map((cv, index) => (
            <ListGroup.Item
              key={index}
              action
              onClick={() => {
                setActive(index);
                if (index !== active) {
                  setUrl(URL.createObjectURL(data[index]));
                }
              }}
              // active={active === index}
              style={
                active === index
                  ? {
                      color: '#7C83FD',
                      borderColor: '#cff4fc',
                      background: '#cff4fc'
                    }
                  : {}
              }
            >
              <div style={{ justifyContent: 'space-between', display: 'flex' }}>
                {cv.name}
                <button
                  type="button"
                  className="btn-close"
                  aria-label="Close"
                  onClick={e => {
                    e.stopPropagation();
                    removeCV(index);
                  }}
                ></button>
              </div>
            </ListGroup.Item>
          ))}
        </ListGroup>
      </Tab.Container>
    </div>
  );
};

export default ListCV;
