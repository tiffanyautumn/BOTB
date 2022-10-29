import { useState } from "react"
import { useNavigate } from "react-router-dom"
import { Button, Card, CardBody, FormGroup, Input, Label, Form } from "reactstrap"
import { addBrand } from "../../modules/brandManager"
export const BrandForm = () => {
    const navigate = useNavigate()
    const [brand, updateBrand] = useState({
        name: ""

    })

    const handleSaveButtonClick = (event) => {
        event.preventDefault()

        const brandToSend = {
            name: brand.name

        }

        return addBrand(brandToSend)
            .then(() => {
                navigate("/brand")
            })
    }
    return (<>
        <Card>
            <CardBody>

                <Form className="brandForm">
                    <FormGroup>
                        <Label for="name">Brand</Label>
                        <Input
                            id="name"
                            name="name"
                            type="text"
                            value={brand.name}
                            onChange={
                                (evt) => {
                                    const copy = { ...brand }
                                    copy.name = evt.target.value
                                    updateBrand(copy)
                                }
                            } />
                    </FormGroup>

                    <Button onClick={(clickEvent) => handleSaveButtonClick(clickEvent)}
                        className="btn btn-primary">
                        Save
                    </Button>
                </Form>

            </CardBody>
        </Card>
    </>)
}