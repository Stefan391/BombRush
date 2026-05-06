import { useState } from "react";
import styles from "./Pages.module.css";
import { RoomControl } from "../components/Home/RoomControl";
import { RoomFinder } from "../components/Home/RoomFinder";
import { Header } from "../components/Home/Header";
import { Footer } from "../components/Home/Footer";

export const HomePage = () => {
  return (
    <div className={styles.HomeRoot}>
      <Header />
      <div className={styles.HomeHolder}>
        <div className={styles.HomeBanner}></div>
        <RoomControl />
        <RoomFinder />
      </div>
      <Footer />
    </div>
  );
};
