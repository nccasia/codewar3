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
      console.log('refFile.current?.files', refFile.current?.files);
      setFiles(Array.from(refFile.current.files));
      // const arrFiles = await Promise.all(
      //   Array.from(refFile.current.files).map(async e => {
      //     const id = uuidv4();
      //     return {
      //       fileId: id,
      //       file: e,
      //       name: e.name.slice(0, e.name.lastIndexOf('.')),
      //       description: '',
      //       type: '',
      //       magicBytes: acceptedFormats.csv.includes(e.type)
      //         ? ''
      //         : await getMagicBytesOfFile(e, event),
      //       thumbnail: await getThumbnailOfPdf(e, id, event)
      //     };
      //   })
      // );
      // props.onChangeFile(
      //   props.multiple ? [...props.files, ...arrFiles] : arrFiles
      // );
      // const target = event.currentTarget;
      // if (target) {
      //   target.value = '';
      // }
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

      <Form.Group className="mb-3">
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
  variant = 'outline-primary',
  options = [],
  value,
  onChange,
  disabled = false
}) => {
  return (
    <Dropdown disabled={disabled}>
      <Dropdown.Toggle style={{ width }} variant={variant} id="dropdown-basic">
        {value || label}
      </Dropdown.Toggle>

      <Dropdown.Menu style={{ width }}>
        {options.map(item => (
          <Dropdown.Item
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
