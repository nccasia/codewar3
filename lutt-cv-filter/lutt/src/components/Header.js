const Header = () => {
  return (
    <>
      <div style={{ display: 'flex', padding: '40px 5%' }}>
        <h1
          style={{
            margin: 0,
            fontFamily: 'Tangerine',
            fontSize: 'xxx-large'
          }}
        >
          Easy as Lutt
        </h1>
        <div
          style={{
            flex: 1,
            borderBottom: 'solid 2px black',
            marginLeft: 25,
            marginBottom: 15
          }}
        ></div>
      </div>
      <h1
        style={{
          textAlign: 'center',
          fontFamily: 'Pacifico',
          color: '#FFB1B1',
          fontSize: 'xxx-large'
        }}
      >
        Lutt
      </h1>
    </>
  );
};

export default Header;
