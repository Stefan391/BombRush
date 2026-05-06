import "./App.css";
import { HomePage } from "./pages/HomePage";
import { GamePage } from "./pages/GamePage";

export default function App() {
  return (
    <div className="AppHolder">
      {/* <HomePage /> */}
      <GamePage />
    </div>
  );
}
