import { Header } from "./components/Header/Header";
import { Footer } from "./components/Footer/Footer";
import "./App.css";
import { HomePage } from "./pages/HomePage";

export default function App() {
  return (
    <div className="AppHolder">
      <Header />
      <HomePage />
      <Footer />
    </div>
  );
}
