import { useState } from "react";
import { RoomTicket } from "./RoomTicket";
import styles from "../Components.module.css";

export const RoomFinder = () => {
  const [els, setEls] = useState({
    playerNum: 966,
    publicRoomNum: 82,
    privateRoomNum: 343,
    rooms: [
      {
        Id: 1,
        Name: "ben's room",
        PlayerCount: 9,
        GameType: "💣 BombParty (English)",
        Code: "TEYH",
      },
      {
        Id: 2,
        Name: "ben's room",
        PlayerCount: 9,
        GameType: "💣 BombParty (English)",
        Code: "TEYH",
      },
      {
        Id: 3,
        Name: "ben's room",
        PlayerCount: 9,
        GameType: "💣 BombParty (English)",
        Code: "TEYH",
      },
      {
        Id: 4,
        Name: "ben's room",
        PlayerCount: 9,
        GameType: "💣 BombParty (English)",
        Code: "TEYH",
      },
      {
        Id: 5,
        Name: "ben's room",
        PlayerCount: 9,
        GameType: "💣 BombParty (English)",
        Code: "TEYH",
      },
      {
        Id: 6,
        Name: "ben's room",
        PlayerCount: 9,
        GameType: "💣 BombParty (English)",
        Code: "TEYH",
      },
      {
        Id: 7,
        Name: "ben's room",
        PlayerCount: 9,
        GameType: "💣 BombParty (English)",
        Code: "TEYH",
      },
    ],
  });

  return (
    <div className={styles.Rooms}>
      <div className={styles.header}>
        <header>
          {els.playerNum} players in {els.publicRoomNum} public rooms and{" "}
          {els.privateRoomNum} private rooms
        </header>
        <div className={styles.filter}>
          <input
            type="text"
            className={styles.styled}
            placeholder={"Search..."}
          />
        </div>
        <div className={styles.refresh}>
          <button className={`${styles.styled} ${styles.blue}`}>Refresh</button>
        </div>
      </div>
      <div className={styles.RoomList}>
        <div className={styles.list}>
          {els.rooms.map((el) => {
            return <RoomTicket el={el} key={el.Id} />;
          })}
        </div>
      </div>
    </div>
  );
};
