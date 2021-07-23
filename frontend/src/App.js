import './App.css';
import { Home } from './pages/Home';
import {
  BrowserRouter as Router,
  Switch,
  Route,
  Link
} from "react-router-dom";
import { Library } from './pages/Library';
import { LuckyDraw } from './pages/Luckydraw';

function App() {
  return (
    <div className="App">
      <Router>
        <Switch>
          <Route exact path="/">
            <Home />
          </Route>
          <Route path="/lib">
            <Library />
          </Route>
          <Route path="/opentalk">
            <LuckyDraw />
          </Route>
        </Switch>
      </Router>
    </div>
  );
}

export default App;
