import { useState } from "react";
import styles from "./Pages.module.css";
import { RoomControl } from "../components/Home/RoomControl";
import { RoomFinder } from "../components/Home/RoomFinder";

export const HomePage = () => {
  return (
    <div className={styles.HomeHolder}>
      <div className={styles.HomeBanner}></div>
      <RoomControl />
      <RoomFinder />
    </div>
  );
};
