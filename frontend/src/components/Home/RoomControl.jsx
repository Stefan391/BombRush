import { RoomCreate } from "./RoomCreate";
import { RoomJoin } from "./RoomJoin";
import styles from "../Components.module.css";

export const RoomControl = () => {
  return (
    <div className={styles.CreateRoom}>
      <div className={styles.CreateRoomLeft}>
        <RoomCreate />
      </div>
      <div className={styles.CreateRoomRight}>
        <RoomJoin />
      </div>
    </div>
  );
};
