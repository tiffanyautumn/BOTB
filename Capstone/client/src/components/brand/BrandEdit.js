import { useEffect, useState } from "react"
import { useNavigate } from "react-router-dom"
import { Button, Card, CardBody, FormGroup, Input, Label, Form } from "reactstrap"
import { editBrand, getBrandById } from "../../modules/brandManager"

export const BrandEdit = ({ Brand, setEditForm, getBrands }) => {
    const navigate = useNavigate()
    const [brand, updateBrand] = useState({
        name: "",
        id: 0

    })

    const getBrand = () => {
        getBrandById(Brand.id).then(b => updateBrand(b))
    }

    useEffect(
        () => {
            getBrand()
        }, []
    )

    const handleSaveButtonClick = (event) => {
        event.preventDefault()

        const brandToSend = {
            name: brand.name,
            id: brand.id

        }

        return editBrand(brand.id, brandToSend)
            .then(() => {
                setEditForm(false);
                getBrands();
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