import classNames from 'classnames';
import { useState } from 'react';

import classes from './App.styles.module.scss';
import reactLogo from './assets/react.svg';

function App() {
  const [count, setCount] = useState(0);

  return (
    <div>
      <div>
        <a href="https://vitejs.dev" target="_blank" rel="noreferrer">
          <img src="/vite.svg" className={classes.logo} alt="Vite logo" />
        </a>

        <a href="https://reactjs.org" target="_blank" rel="noreferrer">
          <img src={reactLogo} className={classNames(classes.logo, classes.react)} alt="React logo" />
        </a>
      </div>

      <h1>Vite + React</h1>

      <div className="card">
        <button onClick={() => setCount(count => count + 1)}>count is {count}</button>

        <p>
          Edit <code>src/App.tsx</code> and save to test HMR
        </p>
      </div>

      <p className="read-the-docs">Click on the Vite and React logos to learn more</p>
    </div>
  );
}

export default App;
