import styles from "../Components.module.css";

export const RoomJoin = () => {
  return (
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
  );
};
