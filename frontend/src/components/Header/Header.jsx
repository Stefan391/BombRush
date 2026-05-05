import styles from "../Components.module.css";

export const Header = () => {
  return (
    <div className={styles.HeaderHolder}>
      <h1>
        <a href="/">Bomb Rush</a>
      </h1>
      <div className={styles.HeaderLoginPartial}>
        <span>Login</span>
      </div>
    </div>
  );
};
