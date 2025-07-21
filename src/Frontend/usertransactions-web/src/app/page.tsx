"use client";

import { Footer } from "./_components/footer";
import { Header } from "./_components/Header";
import { Section } from "./_components/Section";

export default function Home() {
  return (
    <div className="min-h-screen flex flex-col">
      <Header />
      <Section/>
      <Footer/>
    </div>
  );
}
