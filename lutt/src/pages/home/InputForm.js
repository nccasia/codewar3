import { useRef } from 'react';
import { Button, Dropdown, Form } from 'react-bootstrap';
import languages from '../../mock/languages';
import positions from '../../mock/positions';
import levels from '../../mock/levels';

const width = '222px';
const InputForm = ({ setFiles, state, setState }) => {
  const refFile = useRef(null);

  const onFile = () => {
    refFile?.current?.click();
  };

  const onFileChange = async event => {
    if (refFile.current?.files?.length) {
      console.log('refFile.current?.files', refFile.current);
      setFiles(Array.from(refFile.current.files));
      event.target.value = null;
    }
  };

  return (
    <Form>
      <Form.Group className="mb-3">
        <Select
          options={positions}
          value={state.position}
          onChange={position =>
            setState({ ...state, position, language: undefined })
          }
          label="Position"
          width={width}
        />
      </Form.Group>

      <Form.Group className="mb-3">
        <Select
          options={languages[state.position]}
          value={state.language}
          onChange={language => setState({ ...state, language })}
          label="Language"
          width={width}
          disabled={!state.position}
        />
      </Form.Group>

      <Form.Group className="mb-3">
        <Select
          options={levels}
          value={state.level}
          onChange={level => setState({ ...state, level })}
          label="Level"
          width={width}
        />
      </Form.Group>

      <Form.Group>
        <input
          ref={refFile}
          type="file"
          multiple={true}
          hidden
          accept={'.pdf'}
          onChange={onFileChange}
          style={{ display: 'none' }}
          disabled={false}
        />
        <Button style={{ width: width }} onClick={onFile}>
          Upload CV
        </Button>
      </Form.Group>
    </Form>
  );
};

export default InputForm;

const Select = ({
  width = 'auto',
  label = 'Select',
  variant = 'primary',
  options = [],
  value,
  onChange,
  disabled = false
}) => {
  console.log(disabled);
  return (
    <Dropdown>
      <Dropdown.Toggle style={{ width }} variant={variant} disabled={disabled}>
        {value || label}
      </Dropdown.Toggle>

      <Dropdown.Menu style={{ width }}>
        {options.map((item, key) => (
          <Dropdown.Item
            key={key}
            active={item === value}
            onClick={() => {
              onChange(item);
            }}
          >
            {item}
          </Dropdown.Item>
        ))}
      </Dropdown.Menu>
    </Dropdown>
  );
};
