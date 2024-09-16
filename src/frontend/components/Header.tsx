import "./styles/_header.css"

export default function Header() {
    return (
        <nav className="header">
            <div className="header__main-menu">
                <div>Partners</div>
                <div>Blog</div>
                <div>Quick Try</div>
            </div>
            <div className="header__auth-menu">
                <div>Log in</div>
                <div>Sign in</div>
            </div>
        </nav>
    )
}