import { useState } from "react";
import styles from "../Components.module.css";

export const SplitButton = ({ options, setSelected }) => {
  const [selected, setS] = useState(0);

  return (
    <>
      <label
        className={selected == 0 ? styles.selected : ""}
        onClick={() => setS(0)}
      >
        <span>{options.first}</span>
      </label>
      <label
        className={selected == 1 ? styles.selected : ""}
        onClick={() => setS(1)}
      >
        <span>{options.second}</span>
      </label>
    </>
  );
};
