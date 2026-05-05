import styles from "../Components.module.css";

export const RoomTicket = ({ el }) => {
  return (
    <a href="/" className={styles.item} key={el.Id}>
      <div className={styles.info}>
        <div className={styles.title}>
          <span className={styles.text}>{el.Name}</span>
          <span className={styles.playerCount}>{el.PlayerCount}</span>
        </div>
        <div className={styles.playing}>{el.GameType}</div>
      </div>
      <div className={styles.stub}>
        <div className={styles.code}>{el.Code}</div>
        <div className={styles.cutout}></div>
      </div>
    </a>
  );
};
