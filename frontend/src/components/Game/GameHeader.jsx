import styles from "../Components.module.css";

export const GameHeader = () => {
  return (
    <div className={styles.GameHeader}>
      <div className={styles.GameHeaderLeft}>
        <div className={styles.GameCode}>
          <span>
            bomb.rush/<span className={styles.GameCodeBlurred}>●●●●</span> 🔒
          </span>
        </div>
        <div className={styles.GameInfo}>
          <span className={styles.GamePlayerNum}>1</span>{" "}
          <span className={styles.GameOwnerInfo}>Guest3186's room</span>
        </div>
      </div>
      <div className={styles.GameHeaderRight}></div>
    </div>
  );
};
