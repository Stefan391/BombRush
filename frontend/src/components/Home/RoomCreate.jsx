import { useState } from "react";
import styles from "../Components.module.css";
import { SplitButton } from "../InputControls/SplitButton";

export const RoomCreate = () => {
  const [splitOptions] = useState({ first: "🌎 Public", second: "🔒 Private" });
  const [selectedSplit, setSelectedSplit] = useState(0);

  return (
    <div className={styles.CreateRoomBox}>
      <div>
        <header>Start a new room</header>
        <div className={styles.GameSelection}>
          <div className={styles.BombRush}>
            <label>
              <div className={styles.icon}>💣</div>
              <div className={styles.name}>BombRush</div>
              <div className={styles.description}>Explore word game</div>
            </label>
          </div>
        </div>
        <div className={styles.line}>
          <input type="text" placeholder="Name" className={styles.input} />
          <SplitButton options={splitOptions} setSelected={setSelectedSplit} />
          <button className={styles.styled}>Play</button>
        </div>
      </div>
    </div>
  );
};
