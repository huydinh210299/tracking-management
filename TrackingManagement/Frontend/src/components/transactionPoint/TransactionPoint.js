/* eslint-disable no-undef */
import {
  GoogleMap,
  useJsApiLoader,
  Marker,
  InfoWindow,
  Autocomplete,
} from "@react-google-maps/api"
import { useState, useEffect } from "react"
import {
  Select,
  Input,
  Typography,
  Table,
  Drawer,
  Button,
  Row,
  Col,
  Form,
  notification,
  Card,
  Space,
} from "antd"
import { CheckOutlined, CloseCircleOutlined } from "@ant-design/icons"

import { RECORD_MODE } from "../../const/mode"
import { transactionPointConfig } from "../../config/transactionPoint"
import makeRequest from "../../utils/makeRequest"
import omitNil from "../../utils/omit"
import { addListKey } from "../../utils/addListKey"
import { markerIcon } from "../../config/markerIcon"
import SearchText from "../seachItem/SearchText"
import { requestUrl } from "../../resource/requestUrl"
import CustomSkeleton from "../skeleton/CustomSkeleton"
//css
import "../../utils/antdInput.css"

const { Option } = Select
const { Text } = Typography

//  key : AIzaSyBazxC6XMRFWfk4LZGriBCmUTq_wsnl6OU
//  key : AIzaSyDJx44jiP9QHAeOec2C0aLW29jiL6OQyRU
//  key : AIzaSyDurZQBXjtSzKeieXwtFeGe-jhZu-HEGQU
const libraries = ["places"]
const TransactionPoint = () => {
  const [myMap, setMyMap] = useState(null)
  const [center, setCenter] = useState({ lat: 21.0070303, lng: 105.840942 })
  const [visible, setVisible] = useState(false)
  const [points, setPoints] = useState([])
  const [userUnits, setUserUnits] = useState([])
  const [selectedRowArr, setSelectedRowArr] = useState([])
  const [mode, setMode] = useState(RECORD_MODE.CREATE)
  const [selectedRecord, setSelectedRecord] = useState({})
  const [isMark, setIsMark] = useState(false)
  const [selectedPoint, setSelectedPoint] = useState({})
  const [showInfoBox, setShowInfoBox] = useState(false)
  const [autoComplete, setAutoComplete] = useState(null)
  const [loading, setLoading] = useState(false)
  const [pagination, setPagination] = useState({
    current: 1,
    pageSize: 10,
    total: 0,
    showTotal: (total, range) => `${range[0]}-${range[1]} of ${total}`,
  })
  const [filter, setFilter] = useState({
    pointCode: "",
    pointName: "",
    address: "",
  })
  const [lastFilter, setLastFilter] = useState({
    pointCode: "",
    pointName: "",
    address: "",
  })

  const [form] = Form.useForm()

  useEffect(() => {
    const getPoints = async () => {
      const [pointRs, userUnitRs] = await Promise.all([
        makeRequest({
          method: "GET",
          url: requestUrl.transactionPoint.readUrl(),
        }),
        makeRequest({
          method: "GET",
          url: requestUrl.userUnit.readUrl(),
        }),
      ])

      setPagination({
        ...pagination,
        total: pointRs.totalRecords,
      })
      setUserUnits(addListKey(userUnitRs.data))
      setPoints(addListKey(pointRs.data))
      setLoading(false)
    }

    getPoints()
  }, [])

  // set up map
  const { isLoaded } = useJsApiLoader({
    googleMapsApiKey: process.env.REACT_APP_GGMAP_KEY,
    libraries,
  })

  const onLoad = (autocomplete) => {
    setAutoComplete(autocomplete)
  }

  const onPlaceChanged = () => {
    if (autoComplete !== null) {
      const place = autoComplete.getPlace()
      console.log(place)
      const { lat, lng } = place.geometry.location
      const address = place.formatted_address
      form.setFieldsValue({
        longtitude: lng(),
        latitude: lat(),
        address,
      })
    } else {
      console.log("Autocomplete is not loaded yet!")
    }
  }

  //Handle search
  const handleSearch = async (params, pagination = {}) => {
    let filter = omitNil(params)
    setLoading(true)
    makeRequest({
      method: "GET",
      url: requestUrl.transactionPoint.readUrl(),
      params: {
        ...filter,
        page: pagination.current,
        record: pagination.pageSize,
      },
    }).then((searchRs) => {
      setPoints(addListKey(searchRs.data))
      setPagination({
        ...pagination,
        total: searchRs.totalRecords,
      })
      setLoading(false)
    })
  }

  const handlePaginationChange = (page, pageSize) => {
    let newPagination = { ...pagination, current: page, pageSize }
    handleSearch(filter, newPagination)
  }

  const onRowSelected = (record, selected, selectedRows) => {
    setVisible(true)
    let selectedRecord = selectedRows[0]
    console.log(selectedRecord)
    setSelectedRecord(selectedRecord)
    form.resetFields()
    form.setFieldsValue(selectedRecord)
    setMode(RECORD_MODE.UPDATE)
  }

  const onSelectedChange = (selectedRowKeys, selectedRows) => {
    setSelectedRowArr(selectedRowKeys)
  }

  // Function handle drawer
  const onClose = () => {
    setVisible(false)
    setSelectedRowArr([])
    setSelectedRecord({})
    setVisible(false)
  }

  const columns = [
    {
      title: "M?? ??i???m",
      dataIndex: "pointCode",
      key: "pointCode",
      width: 150,
    },
    {
      title: "T??n ??i???m",
      dataIndex: "pointName",
      key: "pointName",
      width: 150,
    },
    {
      title: "?????a ch???",
      dataIndex: "address",
      key: "address",
      ellipsis: true,
    },
    {
      title: "Xo??",
      render: (text, record) => (
        <CloseCircleOutlined style={{ color: "red" }} />
      ),
      align: "center",
      width: 50,
    },
  ]

  const onFinish = (point) => {
    setLoading(true)
    let method = mode === RECORD_MODE.CREATE ? "POST" : "PUT"
    let url =
      mode === RECORD_MODE.CREATE
        ? requestUrl.transactionPoint.createUrl()
        : requestUrl.transactionPoint.updateUrl({ id: point.id })
    let msg =
      mode === RECORD_MODE.CREATE
        ? "Th??m ??i???m m???i th??nh c??ng"
        : "C???p nh???t th??nh c??ng"
    makeRequest({
      method,
      url,
      data: point,
    }).then((rs) => {
      notification.open({
        message: "Th??ng b??o",
        icon: <CheckOutlined style={{ color: "#2fd351" }} />,
        description: msg,
      })
      handleSearch(filter, pagination)
      setSelectedRowArr([])
      setSelectedRecord({})
      setVisible(false)
      setLoading(false)
    })
  }

  const onOpenDrawer = () => {
    setMode(RECORD_MODE.CREATE)
    setSelectedRecord({})
    form.resetFields()
    form.setFieldsValue({})
    setVisible(true)
  }

  const onRowClick = (record) => {
    setCenter({ lat: record.latitude, lng: record.longtitude })
    setSelectedPoint(record)
    setIsMark(true)
  }

  return (
    <Row style={{ height: "100%" }} gutter={16}>
      <Col span={15}>
        {isLoaded && (
          <GoogleMap
            mapContainerStyle={{
              height: "100%",
              width: "100%",
            }}
            zoom={17}
            center={center}
            onLoad={(map) => setMyMap(map)}
          >
            {isMark && (
              <Marker
                icon={{
                  url: markerIcon[selectedPoint.pointType],
                  size: { width: 44, height: 20 },
                }}
                animation="DROP"
                shape="MarkerShapeRect"
                clickable={true}
                position={center}
                onClick={() => setShowInfoBox(true)}
              ></Marker>
            )}
            {showInfoBox && (
              <InfoWindow
                position={center}
                onCloseClick={() => setShowInfoBox(false)}
                options={{ pixelOffset: new google.maps.Size(0, -20) }}
              >
                <div
                  style={{
                    background: `white`,
                    border: `1px solid #ccc`,
                    padding: 7,
                    width: 200,
                    minHeight: 100,
                  }}
                >
                  {selectedPoint.address}
                </div>
              </InfoWindow>
            )}
          </GoogleMap>
        )}
      </Col>
      <Col span={9}>
        <Card
          title="Danh s??ch ??i???m giao d???ch"
          extra={
            <Button type="primary" onClick={onOpenDrawer}>
              Th??m ??i???m giao d???ch
            </Button>
          }
          style={{ height: "100%" }}
        >
          <Row gutter={[16, 16]} style={{ marginBottom: 20 }}>
            <Col span={8}>
              <Text>M?? ??i???m</Text>
            </Col>
            <Col span={16}>
              <SearchText
                dataIndex="pointCode"
                placeholder="??i???n m?? ??i???m"
                filter={filter}
                lastFilter={lastFilter}
                setFilter={setFilter}
                setLastFilter={setLastFilter}
                handleSearch={handleSearch}
                pagination={pagination}
                style={{ width: "100%" }}
              ></SearchText>
            </Col>
          </Row>
          <Table
            columns={columns}
            dataSource={points}
            size="small"
            pagination={{
              position: ["bottomRight"],
              ...pagination,
              onChange: handlePaginationChange,
            }}
            rowSelection={{
              selectedRowKeys: selectedRowArr,
              columnTitle: <div>S???a</div>,
              columnWidth: 50,
              type: "checkbox",
              onSelect: (record, selected, selectedRows) =>
                onRowSelected(record, selected, selectedRows),
              onChange: (selectedRowKeys, selectedRows) =>
                onSelectedChange(selectedRowKeys, selectedRows),
            }}
            onRow={(record) => ({
              onClick: () => onRowClick(record),
            })}
          ></Table>
        </Card>
      </Col>
      <Drawer
        title="Th??ng tin ??i???m"
        placement="right"
        visible={visible}
        width="500"
        onClose={onClose}
      >
        <Form
          labelCol={{ span: 6 }}
          wrapperCol={{ span: 18 }}
          form={form}
          labelAlign="left"
          initialValues={selectedRecord}
          onFinish={onFinish}
        >
          <Form.Item label="id" name="id" noStyle>
            <Input type="hidden" />
          </Form.Item>
          <Form.Item
            name="unitId"
            label="????n v??? qu???n l??"
            rules={[
              {
                required: true,
                message: "Tr?????ng n??y kh??ng ???????c thi???u",
              },
            ]}
          >
            <Select>
              {userUnits.map((item, index) => (
                <Option value={item.id} key={index}>
                  {item.name}
                </Option>
              ))}
            </Select>
          </Form.Item>
          <Form.Item
            name="pointType"
            label="Lo???i ??i???m"
            rules={[
              {
                required: true,
                message: "Tr?????ng n??y kh??ng ???????c thi???u",
              },
            ]}
          >
            <Select>
              {transactionPointConfig.type.map((item, index) => (
                <Option value={item.value} key={index}>
                  {item.label}
                </Option>
              ))}
            </Select>
          </Form.Item>
          <Form.Item label="M?? ??i???m" name="pointCode">
            <Input></Input>
          </Form.Item>
          <Form.Item label="T??n ??i???m" name="pointName">
            <Input></Input>
          </Form.Item>
          <Form.Item label="?????a ??i???m" name="address">
            <Autocomplete
              onLoad={onLoad}
              onPlaceChanged={onPlaceChanged}
              fields={["geometry.location", "formatted_address"]}
              restrictions={{ country: "vn" }}
            >
              <input
                type="text"
                placeholder="??i???n ?????a ??i???m"
                className="antd-input"
                defaultValue={selectedRecord.address}
              />
            </Autocomplete>
          </Form.Item>
          <Form.Item label="Kinh ?????" name="longtitude">
            <Input></Input>
          </Form.Item>
          <Form.Item label="V?? d???" name="latitude">
            <Input></Input>
          </Form.Item>
          <Form.Item label="Ng?????i li??n h???" name="contact">
            <Input></Input>
          </Form.Item>
          <Form.Item label="??i???n tho???t" name="phone">
            <Input></Input>
          </Form.Item>
          <Form.Item label="Fax" name="fax">
            <Input></Input>
          </Form.Item>

          <Form.Item
            wrapperCol={{
              offset: 0,
              span: 24,
            }}
          >
            <Button type="primary" htmlType="submit">
              L??u
            </Button>
          </Form.Item>
        </Form>
      </Drawer>
      <CustomSkeleton loading={loading} />
    </Row>
  )
}

export default TransactionPoint
