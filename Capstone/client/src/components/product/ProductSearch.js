import { useState } from "react"
import { useNavigate } from "react-router-dom"

export const ProductSearch = ({ setSearchTerms }) => {
    const navigate = useNavigate()
    return (
        <div>
            <input
                onChange={
                    (changeEvent) => {
                        setSearchTerms(changeEvent.target.value)
                    }
                }
                type="text" placeholder="Enter search terms"></input>
            {/* <button onClick={(() => navigate(`/product/${searchTermState}`))}>search</button> */}
        </div>
    )
}

