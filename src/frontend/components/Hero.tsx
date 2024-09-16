import { Button } from "antd";
import "./styles/_hero.css"
import Link from "next/link";

export default function Hero() {
    return (
        <div className="hero" style={{ display: "flex", flexDirection: "column", alignItems: "center", textAlign: "center" }}>
            <div className="hero__content">
                <h1>Illuminate Your Language Skills with Luminos Language</h1>
                <h4>Test your proficency through engaging assessments in grammar, listening, and speaking, and receive instant feedback to guide your learning journey</h4>

                <Button>
                    <Link href="/assessment/introduction">Start your Journey</Link>
                </Button>
            </div>

            <div className="hero__image">
                <div className="overlay"></div>
                <img src="/hero.png" alt="hero" />
            </div>

        </div>
    )
}