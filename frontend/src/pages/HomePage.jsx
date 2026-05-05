import { useState } from "react";
import styles from "./Pages.module.css";

export const HomePage = () => {
  const [els, setEls] = useState([1, 2, 3, 4, 5, 6, 7]);

  return (
    <div className={styles.HomeHolder}>
      <div className={styles.HomeBanner}></div>
      <div className={styles.CreateRoom}>
        <div className={styles.CreateRoomLeft}>
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
                <input
                  type="text"
                  placeholder="Name"
                  className={styles.input}
                />
                <label>
                  🌎
                  <span>Public</span>
                </label>
                <label>
                  🔒
                  <span>Private</span>
                </label>
                <button className={styles.styled}>Play</button>
              </div>
            </div>
          </div>
        </div>
        <div className={styles.CreateRoomRight}>
          <div className={styles.JoinRoom}>
            <div>
              <header>Join a private room</header>
              <div className={styles.line}>
                <div className={styles.label}>Code: </div>
                <input type="text" className={styles.styled} />
                <div>
                  <button className={styles.styled}>Join</button>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div className={styles.Rooms}>
        <div className={styles.header}>
          <header>
            Play with 966 players in 82 public rooms and 343 private rooms
          </header>
          <div className={styles.filter}>
            <input
              type="text"
              className={styles.styled}
              placeholder={"Search..."}
            />
          </div>
          <div className={styles.refresh}>
            <button className={styles.styled}>Refresh</button>
          </div>
        </div>
        <div className={styles.RoomList}>
          <div className={styles.list}>
            {els.map((el) => {
              return (
                <a href="/" className={styles.item} key={el}>
                  <div className={styles.info}>
                    <div className={styles.title}>
                      <span className={styles.text}>ben's room</span>
                      <span className={styles.playerCount}>9</span>
                    </div>
                    <div className={styles.playing}>💣 BombParty (English)</div>
                  </div>
                  <div className={styles.stub}>
                    <div className={styles.code}>TEYH</div>
                    <div className={styles.cutout}></div>
                  </div>
                </a>
              );
            })}
          </div>
        </div>
      </div>
    </div>
  );
};
